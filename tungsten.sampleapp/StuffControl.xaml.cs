using System.Windows;
using System.Windows.Controls;

namespace tungsten.sampleapp
{
    /// <summary>
    /// Interaction logic for StuffControl.xaml
    /// </summary>
    public partial class StuffControl : UserControl
    {
        public StuffControl()
        {
            InitializeComponent();
        }

        private void BtnSubmit_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Submit submitted", "Title", MessageBoxButton.OKCancel);
        }
    }
}
