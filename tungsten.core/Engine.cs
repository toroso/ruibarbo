using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using tungsten.core.ElementFactory;

namespace tungsten.core
{
    public class Engine
    {
        private Thread _uiThread;
        private Dispatcher _dispatcher;
        private readonly ElementFactory.ElementFactory _elementFactory = new ElementFactory.ElementFactory();
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
                });
        }

        public void ConfigureElementFactory(Action<IElementFactoryConfigurator> cfgAction)
        {
            cfgAction(new ElementFactoryConfigurator(_elementFactory));
        }

        public void Start(IApplication application)
        {
            _uiThread = new Thread(() =>
                {
                    try
                    {
                        if (Application.Current == null)
                        {
                            // TODO: Use Application.Current? What if there's another Application running?
                            new Application();
                        }

                        _dispatcher = Dispatcher.CurrentDispatcher;
                        _dispatcher.UnhandledException += OnCurrentDispatcherUnhandledException;
                        AppDomain.CurrentDomain.UnhandledException += OnCurrentDomainUnhandledException;
                        SynchronizationContext.SetSynchronizationContext(new DispatcherSynchronizationContext(_dispatcher));

                        Desktop = new DesktopElement(_dispatcher, _elementFactory, Application.Current);

                        //EnsureApplicationResources();

                        // TODO: try-catch around this one only?
                        application.Start();

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

            // TODO: Wait for allication.Start() to finish on other thread
            Thread.Sleep(1000);
        }

        public void ShutDown()
        {
            _dispatcher.BeginInvokeShutdown(DispatcherPriority.Send);
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
