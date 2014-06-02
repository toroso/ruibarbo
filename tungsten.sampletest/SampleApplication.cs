using tungsten.core;
using tungsten.sampleapp;

namespace tungsten.sampletest
{
    public class SampleApplication : IApplication
    {
        public void Start()
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}