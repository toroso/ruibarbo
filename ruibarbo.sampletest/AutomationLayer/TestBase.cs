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

        [SetUp]
        public void SetUp()
        {
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
            Engine.Start(new SampleApplication());
        }

        [TearDown]
        public void TearDown()
        {
            Engine.ShutDown();
            CollectionAssert.IsEmpty(Engine.UnhandledExceptions);
        }

        protected MainWindow MainWindow
        {
            get { return Engine.Desktop.FindFirstChild<MainWindow>(By.Name("WndMain")); }
        }

        protected MessageBox MessageBox
        {
            get { return Engine.Desktop.FindFirstChild<MessageBox>(By.Class("#32770")); }
        }
    }
}
