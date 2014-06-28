using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace tungsten.sampleapp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }

    public class MainViewModel
    {
        public IEnumerable<ItemViewModel> ShortItems
        {
            // Shortened list so that items are not lazy-created (null)
            get { return Items.Take(12); }
        }

        public IEnumerable<ItemViewModel> Items
        {
            get
            {
                return new[]
                    {
                        new ItemViewModel {Start = "0", Muppet = "Animal"},
                        new ItemViewModel {Start = "4", Muppet = "Fozzie Bear"},
                        new ItemViewModel {Start = "8", Muppet = "Pops"},
                        new ItemViewModel {Start = "9", Muppet = "Kermit the Frog"},
                        new ItemViewModel {Start = "14", Muppet = "Swedish Chef"},
                        new ItemViewModel {Start = "17", Muppet = "Bubba the Rat"},
                        new ItemViewModel {Start = "19", Muppet = "Miss Piggy"},
                        new ItemViewModel {Start = "24", Muppet = "Johnny Fiama"},
                        new ItemViewModel {Start = "26", Muppet = "Bobo the Bear"},
                        new ItemViewModel {Start = "35", Muppet = "Beaker"},
                        new ItemViewModel {Start = "36", Muppet = "Waldo C. Graphic"},
                        new ItemViewModel {Start = "39", Muppet = "The Great Gonzo"},
                        new ItemViewModel {Start = "42", Muppet = "Annie Sue"},
                        new ItemViewModel {Start = "48", Muppet = "Yolanda the Rat"},
                        new ItemViewModel {Start = "51", Muppet = "Statler"},
                        new ItemViewModel {Start = "53", Muppet = "Waldorf"},
                        new ItemViewModel {Start = "58", Muppet = "George the Janitor"},
                        new ItemViewModel {Start = "60", Muppet = "Scooter"},
                    };
            }
        }
    }

    public class ItemViewModel
    {
        public string Start { get; set; }
        public string Muppet { get; set; }
    }
}
