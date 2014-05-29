using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using tungsten.core.ElementFactory;
using tungsten.core.Elements;

namespace tungsten.core
{
    public class Engine
    {
        private Thread _uiThread;
        private readonly List<Exception> _unhandledExceptions = new List<Exception>();

        public IEnumerable<Exception> UnhandledExceptions
        {
            get
            {
                // TODO: thread safety!
                return new List<Exception>(_unhandledExceptions);
            }
        }

        public DesktopElement Desktop { get; private set; }

        public Engine()
        {
            ConfigureElementFactory(x =>
                {
                    x.For<System.Windows.FrameworkElement>().Create<WpfElement>(); // Fallback
                    x.For<System.Windows.Window>().Create<WpfWindow>();
                    x.For<System.Windows.Controls.Button>().Create<WpfButton>();
                    x.For<System.Windows.Controls.TextBox>().Create<WpfTextBox>();
                });
        }

        public void ConfigureElementFactory(Action<IElementFactoryConfigurator> cfgAction)
        {
            cfgAction(new ElementFactoryConfigurator());
        }

        public void Start(IApplication application)
        {
            var waitHandle = new AutoResetEvent(false);
            Dispatcher dispatcher = null;
            _uiThread = new Thread(() =>
                {
                    try
                    {
                        if (Application.Current == null)
                        {
                            // TODO: Use Application.Current? What if there's another Application running?
                            new Application();
                        }

                        dispatcher = Dispatcher.CurrentDispatcher;
                        dispatcher.UnhandledException += OnCurrentDispatcherUnhandledException;
                        AppDomain.CurrentDomain.UnhandledException += OnCurrentDomainUnhandledException;
                        SynchronizationContext.SetSynchronizationContext(new DispatcherSynchronizationContext(dispatcher));

                        Desktop = new DesktopElement(Application.Current);

                        //EnsureApplicationResources();

                        // TODO: try-catch around this one only?
                        application.Start();

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
