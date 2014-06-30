using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace tungsten.sampleapp
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

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

        public IEnumerable<string> ComboBoxItems
        {
            get
            {
                return new[]
                {
                    "No error",
                    "Has error",
                    "Item 3",
                    "Item 4",
                    "Item 5",
                    "Item 6",
                    "Item 7",
                    "Item 8",
                    "Item 9",
                    "Item 10",
                    "Item 11",
                    "Item 12",
                    "Item 13",
                    "Item 14",
                    "Item 15",
                    "Item 16",
                    "Item 17",
                    "Item 18",
                    "Item 19",
                    "Item 20",
                    "Item 21",
                    "Item 22",
                    "Item 23",
                    "Item 24",
                    "Item 25",
                    "Item 26",
                    "Item 27",
                    "Item 28",
                    "Item 29",
                };
            }
        }

        private string _selectedComboBoxItem;

        public string SelectedComboBoxItem
        {
            get { return _selectedComboBoxItem; }
            set
            {
                _selectedComboBoxItem = value;
                PropertyChanged.Raise(this, vm => vm.SelectedComboBoxItem);
                PropertyChanged.Raise(this, vm => vm.HasError);
            }
        }

        public bool HasError
        {
            get { return ComboBoxItems.IndexOf(SelectedComboBoxItem) == 1; }
        }

        public MainViewModel()
        {
            SelectedComboBoxItem = ComboBoxItems.First();
        }
    }
}