using NUnit.Framework;
using tungsten.core;
using tungsten.core.ElementFactory;
using tungsten.core.Search;
using tungsten.core.Win32.Factory;
using tungsten.core.Wpf.Factory;

namespace tungsten.sampletest.AutomationLayer
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
                            q.AddRegisteredElementsInAssembly(typeof(TestBase).Assembly);
                        }));
                    x.AddFactory(new Win32ControlFactory(q =>
                        {
                            q.AddControl<MessageBox>();
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
