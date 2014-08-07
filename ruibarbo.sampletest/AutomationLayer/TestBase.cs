using System;

using NUnit.Framework;
using ruibarbo.core;
using ruibarbo.core.Search;
using ruibarbo.core.Win32.Factory;
using ruibarbo.core.Wpf.Factory;

namespace ruibarbo.sampletest.AutomationLayer
{
    public class TestBase
    {
        protected Engine Engine;
        private bool _isStarted;

        [SetUp]
        public void SetUp()
        {
            _isStarted = false;
            Engine = new Engine();
            Engine.ConfigureElementFactory(x =>
                {
                    x.AddFactory(new FrameworkElementFactory(q =>
                        {
                            q.AddRegisteredElementsInAssembly(GetType().Assembly);
                        }));
                    x.AddFactory(new Win32ControlFactory(q =>
                        {
                            q.AddRegisteredElementsInAssembly(GetType().Assembly);
                        }));
                });
            Engine.ConfigureHardware(x =>
                {
                    x.DelayWhenOpeningComboBox = TimeSpan.FromMilliseconds(40);
                });
        }

        [TearDown]
        public void TearDown()
        {
            if (_isStarted)
            {
                Engine.ShutDown();
            }

            CollectionAssert.IsEmpty(Engine.UnhandledExceptions);
        }

        protected MainWindow MainWindow
        {
            get
            {
                if (!_isStarted)
                {
                    // Lazy start so that tests can perform setup before application is started.
                    _isStarted = true;
                    Engine.Start(new SampleApplication());
                }

                var mainWindow = Engine.Desktop.FindFirstChild<MainWindow>(By.Name("WndMain"));
                mainWindow.MakeSureWindowIsTopmost();
                return mainWindow;
            }
        }

        protected MessageBox MessageBox
        {
            get { return Engine.Desktop.FindFirstChild<MessageBox>(By.Class("#32770")); }
        }
    }
}
