using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using ruibarbo.core.Common;
using ruibarbo.core.ElementFactory;
using ruibarbo.core.Win32.Factory;
using ruibarbo.core.Win32.Native;
using ruibarbo.core.Wpf.Invoker;
using FrameworkElementFactory = ruibarbo.core.Wpf.Factory.FrameworkElementFactory;

namespace ruibarbo.core
{
    public sealed class Engine
    {
        private readonly object _unhandledExceptionsLock = new object();
        private readonly List<Exception> _unhandledExceptions = new List<Exception>();
        private Thread _uiThread;

        public DesktopElement Desktop { get; private set; }

        public IEnumerable<Exception> UnhandledExceptions
        {
            get
            {
                lock (_unhandledExceptionsLock)
                {
                    return new List<Exception>(_unhandledExceptions);
                }
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
                            q.AddRegisteredElementsInAssembly(GetType().Assembly);
                        }));
                    x.AddFactory(new Win32ControlFactory(q =>
                        {
                            q.AddRegisteredElementsInAssembly(GetType().Assembly);
                        }));
                });
            Configure(x =>
                {
                    x.MaxRetryTime = TimeSpan.FromSeconds(5);
                    x.KeyboardDelayBetweenKeys = TimeSpan.Zero;
                    x.KeyboardDelayAfterTyping = TimeSpan.Zero;
                    x.MouseDelayAfterMove = TimeSpan.FromMilliseconds(40);
                    x.MouseDelayAfterClick = TimeSpan.FromMilliseconds(20);
                    x.MouseDurationOfMove = TimeSpan.Zero;
                    x.DelayWhenOpeningComboBox = TimeSpan.Zero;
                    x.ScreenshotOnFailedAssertion = true;
                });
        }

        public void ConfigureElementFactory(Action<IElementFactoryConfigurator> cfgAction)
        {
            cfgAction(new ElementFactoryConfigurator());
        }

        public void Configure(Action<Configurator> cfgAction)
        {
            cfgAction(new Configurator());
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

            OnUiThread.Create(dispatcher);
            Desktop = new DesktopElement();

            // Ensure resources are working
            Application.ResourceAssembly = OnUiThread.Get(() => application.MainAssembly);
            MergeResources(OnUiThread.Get(application.Resources.ToArray));

            OnUiThread.Invoke(application.Start);
        }

        private static void MergeResources(IEnumerable<Uri> resources)
        {
            foreach (var uri in resources)
            {
                Application.Current.Resources.MergedDictionaries.Add((ResourceDictionary)Application.LoadComponent(uri));
            }
        }

        public void ShutDown()
        {
            Win32Api.CloseAllWindows();
            OnUiThread.BeginInvokeShutdown();
            var wasJoined = _uiThread.Join(TimeSpan.FromMilliseconds(5000));
            if (!wasJoined)
            {
                // If the last thing that happens in a test is that a Win32 window is opened it will open after the CloseAllWindows()
                // call above is made. If a window is open we can't join the threads. Here we make another attempt.
                Win32Api.CloseAllWindows();
                _uiThread.Join(TimeSpan.FromMilliseconds(5000));
            }
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
            lock (_unhandledExceptionsLock)
            {
                _unhandledExceptions.Add(exception);
            }
        }
    }
}
