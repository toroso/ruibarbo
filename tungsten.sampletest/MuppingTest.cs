using System;
using System.Linq;
using NUnit.Framework;
using tungsten.core;
using tungsten.sampleapp;

namespace tungsten.sampletest
{
    [TestFixture]
    public class MuppingTest
    {
        private Engine _engine;

        [SetUp]
        public void SetUp()
        {
            _engine = new Engine();
        }

        [TearDown]
        public void TearDown()
        {
            _engine.ShutDown();
            CollectionAssert.IsEmpty(_engine.UnhandledExceptions);
        }

        [Test]
        public void DoDaThing()
        {
            var myApplication = new MyApplication();
            _engine.Start(myApplication);

            var window = _engine.Desktop.FindFirstElement<WpfWindow>(By.Name("wndMain"));
            Console.WriteLine("Found window, Element name path: '{0}'; class path: {1}", window.ElementNamePath(), window.ElementClassPath());
            var button = window.FindFirstElement<WpfButton>(By.Name("btnClick"));
            Console.WriteLine("Found button, Element name path: '{0}'; class path: {1}", button.ElementNamePath(), button.ElementClassPath());
            Console.WriteLine("Button, Element name or class path: '{0}'", button.ElementNameOrClassPath());
            button.Click();
        }
    }

    public class MyApplication : IApplication
    {
        public void Start()
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}
