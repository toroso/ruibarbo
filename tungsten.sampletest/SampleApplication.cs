using System;
using System.Collections.Generic;
using System.Reflection;
using tungsten.core;
using tungsten.sampleapp;

namespace tungsten.sampletest
{
    public class SampleApplication : IApplication
    {
        public Assembly MainAssembly
        {
            get { return typeof (MainWindow).Assembly; }
        }

        public IEnumerable<Uri> Resources
        {
            get
            {
                return new[]
                    {
                        // Perhaps TODO: Only return Resources.xaml and let the framework handle the rest?
                        new Uri(@"tungsten.sampleapp;;;component/Resources.xaml", UriKind.Relative),
                        // TODO: Add Themes/Generic.xaml
                    };
            }
        }

        public void Start()
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}