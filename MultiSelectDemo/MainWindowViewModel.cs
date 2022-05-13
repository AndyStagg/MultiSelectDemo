using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MultiSelectDemo
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public MainWindowViewModel()
        {
            MyItems = new ObservableCollection<MyItem>();
            MyItems.Add(new MyItem() { Property_One = "One", Property_Two = "Two", Property_Three = "Three" });
            MyItems.Add(new MyItem() { Property_One = "One", Property_Two = "Two", Property_Three = "Three" });
            MyItems.Add(new MyItem() { Property_One = "One", Property_Two = "Two", Property_Three = "Three" });
            MyItems.Add(new MyItem() { Property_One = "One", Property_Two = "Two", Property_Three = "Three" });
        }

        private ObservableCollection<MyItem> _myItems;
        public ObservableCollection<MyItem> MyItems
        {
            get => _myItems;
            set => Set(ref _myItems, value);
        }

        private IList<MyItem> _mySelectedItems;
        public IList<MyItem> MySelectedItems
        {
            get => _mySelectedItems;
            set
            {
                Set(ref _mySelectedItems, value);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected bool Set<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(field, newValue))
            {
                field = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                return true;
            }
            return false;
        }
    }
}
