using NUnit.Framework;
using tungsten.core;
using tungsten.core.ElementFactory;
using tungsten.core.Search;

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
    }
}
