using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using tungsten.core.ElementFactory;
using tungsten.core.Input;
using tungsten.core.Win32;
using tungsten.core.Win32.Factory;
using tungsten.core.Wpf.Base;
using FrameworkElementFactory = tungsten.core.Wpf.Factory.FrameworkElementFactory;

namespace tungsten.core
{
    public class Engine
    {
        private readonly List<Exception> _unhandledExceptions = new List<Exception>();
        private Thread _uiThread;

        public DesktopElement Desktop { get; private set; }

        public IEnumerable<Exception> UnhandledExceptions
        {
            get
            {
                // TODO: thread safety!
                return new List<Exception>(_unhandledExceptions);
            }
        }

        public Engine()
        {
            if (Application.Current == null)
            {
                new Application(); // Sets Application.Current
            }

            ConfigureElementFactory(x =>
                {
                    x.RemoveAllFactories();
                    x.AddFactory(new FrameworkElementFactory(q =>
                        {
                            q.AddRegisteredElementsInAssembly(typeof (WpfFrameworkElementBase<FrameworkElement>).Assembly);
                        }));
                    x.AddFactory(new Win32ControlFactory(q =>
                        {
                            q.AddControl<Win32Control>();
                        }));
                });
            ConfigureHardware(x =>
                {
                    x.KeyboardDelayBetweenKeys = TimeSpan.Zero;
                    x.KeyboardDelayAfterTyping = TimeSpan.Zero;
                    x.MouseDelayAfterMove = TimeSpan.Zero;
                    x.MouseDelayAfterClick = TimeSpan.FromMilliseconds(20);
                    x.MouseDurationOfMove = TimeSpan.Zero;
                    x.ScreenshotOnFailedAssertion = true; // TODO: Implement
                });
        }

        public void ConfigureElementFactory(Action<IElementFactoryConfigurator> cfgAction)
        {
            cfgAction(new ElementFactoryConfigurator());
        }

        public void ConfigureHardware(Action<HardwareConfigurator> cfgAction)
        {
            cfgAction(new HardwareConfigurator());
        }

        public void Start(IApplication application)
        {
            var waitHandle = new AutoResetEvent(false);

            Dispatcher dispatcher = null;
            _uiThread = new Thread(() =>
                {
                    try
                    {
                        dispatcher = Dispatcher.CurrentDispatcher;
                        dispatcher.UnhandledException += OnCurrentDispatcherUnhandledException;
                        AppDomain.CurrentDomain.UnhandledException += OnCurrentDomainUnhandledException;
                        SynchronizationContext.SetSynchronizationContext(new DispatcherSynchronizationContext(dispatcher));

                        waitHandle.Set();

                        // Makes the thread support message pumping
                        Dispatcher.Run();
                    }
                    catch (Exception ex)
                    {
                        // TODO: _unhandledExceptions?
                        Console.WriteLine(ex);
                    }
                });

            // Configure the thread
            _uiThread.SetApartmentState(ApartmentState.STA);
            _uiThread.Name = "UI Thread";

            _uiThread.Start();

            waitHandle.WaitOne();

            Invoker.Create(dispatcher);
            Desktop = new DesktopElement();

            // EnsureApplicationResources();
            // Check http://stackoverflow.com/questions/15548769/instantiate-resourcedictionary-xaml-from-other-assembly

            Invoker.Invoke(application.Start);
        }

        public void ShutDown()
        {
            Invoker.BeginInvokeShutdown();
            _uiThread.Join();
        }

        private void OnCurrentDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            HandleException(e.Exception);
            e.Handled = true;
        }

        private void OnCurrentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            HandleException((Exception)e.ExceptionObject);
        }

        private void HandleException(Exception exception)
        {
            _unhandledExceptions.Add(exception);
        }
    }
}
