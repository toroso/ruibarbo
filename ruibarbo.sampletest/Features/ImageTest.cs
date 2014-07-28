using NUnit.Framework;

using ruibarbo.nunit;
using ruibarbo.sampletest.AutomationLayer;

namespace ruibarbo.sampletest.Features
{
    [TestFixture]
    public class ImageTest : TestBase
    {
        [Test]
        public void FindImage()
        {
            var image = MainWindow.SpaceImage;
            image.AssertThat(x => x.IsVisible, Is.True);
        }
    }
}