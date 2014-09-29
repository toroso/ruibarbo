using System;
using NUnit.Framework;
using ruibarbo.core;
using ruibarbo.core.Debug;
using ruibarbo.core.Search;
using ruibarbo.core.Win32;
using ruibarbo.core.Wpf;

namespace ruibarbo.sampletest
{
    /// <summary>
    /// Some basic tests to demonstrate the library foundation.
    /// </summary>
    [TestFixture]
    public class BasicTest
    {
        private Engine _engine;

        [SetUp]
        public void SetUp()
        {
            _engine = new Engine();
            _engine.Start(new SampleApplication());
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
            var window = _engine.Desktop.FindFirstChild<WpfWindow>(By.Name("WndMain"));

            window.MoveTo(400, 400);

            Console.WriteLine("Found window, Element name path: '{0}'; class path: {1}", window.ElementNamePath(), window.ElementClassPath());
            var button = window.FindFirstChild<WpfButton>(By.Name("BtnSubmit"));
            Console.WriteLine("Found button, Element name path: '{0}'; class path: {1}", button.ElementNamePath(), button.ElementClassPath());
            Console.WriteLine("Button, Element name or class path: '{0}'", button.ElementNameOrClassPath());
            Console.WriteLine("Button, Element search by path: '{0}'", button.ElementSearchPath());
            button.Click();
            var messageBox = _engine.Desktop.FindFirstChild<Win32Control>(By.Class("#32770"));
            var okButton = messageBox.FindFirstChild<Win32Control>(By.Class("Button"));
            okButton.Click();
            // TODO: button.Trigger() to click it programmatically
            var textbox = window.FindFirstChild<WpfTextBox>(By.Name("TxtInput"));
            Console.WriteLine("Found textbox, Element name path: '{0}'; class path: {1}", textbox.ElementNamePath(), textbox.ElementClassPath());
            textbox.Click();
            textbox.Type("Smurf");
            // TODO: textbox.Text = "Smurf";
        }
    }
}
