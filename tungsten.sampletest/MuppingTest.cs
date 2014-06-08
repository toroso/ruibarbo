using System;
using NUnit.Framework;
using tungsten.core.Elements;
using tungsten.core.Search;
using tungsten.core.Utils;
using tungsten.sampletest.AutomationLayer;

namespace tungsten.sampletest
{
    [TestFixture]
    public class MuppingTest : TestBase
    {
        [Test]
        public void DoDaThing()
        {
            var window = Desktop.FindFirstChild<WpfWindow>(By.Name("WndMain"));
            Console.WriteLine("Found window, Element name path: '{0}'; class path: {1}", window.ElementNamePath(), window.ElementClassPath());
            var button = window.FindFirstChild<WpfButton>(By.Name("BtnSubmit"));
            Console.WriteLine("Found button, Element name path: '{0}'; class path: {1}", button.ElementNamePath(), button.ElementClassPath());
            Console.WriteLine("Button, Element name or class path: '{0}'", button.ElementNameOrClassPath());
            Console.WriteLine("Button, Element search by path: '{0}'", button.ElementSearchPath());
            button.Click();
            // TODO: button.Trigger() to click it programmatically
            var textbox = window.FindFirstChild<WpfTextBox>(By.Name("TxtInput"));
            Console.WriteLine("Found textbox, Element name path: '{0}'; class path: {1}", textbox.ElementNamePath(), textbox.ElementClassPath());
            textbox.Click();
            textbox.Type("Smurf");
            // TODO: textbox.Text = "Smurf";
        }
    }
}
