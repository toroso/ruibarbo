using NUnit.Framework;
using tungsten.core;
using tungsten.core.Elements;

namespace tungsten.sampletest
{
    public class TestBase
    {
        protected Engine Engine;

        [SetUp]
        public void SetUp()
        {
            Engine = new Engine();
            Engine.Start(new SampleApplication());
        }

        [TearDown]
        public void TearDown()
        {
            Engine.ShutDown();
            CollectionAssert.IsEmpty(Engine.UnhandledExceptions);
        }

        protected DesktopElement Desktop
        {
            get { return Engine.Desktop; }
        }
    }
}
