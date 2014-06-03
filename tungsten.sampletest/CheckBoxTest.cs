using NUnit.Framework;
using tungsten.core.Elements;
using tungsten.core.Search;
using tungsten.nunit;

namespace tungsten.sampletest
{
    public class CheckBoxTest : TestBase
    {
        [Test]
        public void Hupp()
        {
            var window = Desktop.FindFirstElement<WpfWindow>(By.Name("WndMain"));
            var checkBox = window.FindFirstElement<WpfCheckBox>(By.Name("ShowStuff"));
            checkBox.AssertThat(x => x.IsChecked, Is.True);
        }
    }
}