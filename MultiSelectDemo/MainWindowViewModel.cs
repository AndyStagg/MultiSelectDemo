using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MultiSelectDemo
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        #region Constructors
        public MainWindowViewModel()
        {
            MyItems = new ObservableCollection<MyItem>();
            MyItems.Add(new MyItem() { Property_One = "One", Property_Two = "Two", Property_Three = "Three" });
            MyItems.Add(new MyItem() { Property_One = "One", Property_Two = "Two", Property_Three = "Three" });
            MyItems.Add(new MyItem() { Property_One = "One", Property_Two = "Two", Property_Three = "Three" });
            MyItems.Add(new MyItem() { Property_One = "One", Property_Two = "Two", Property_Three = "Three" });

            // It is critical to make this assignement
            MySelectedItems = new ObservableCollection<MyItem>();
            // It intializes the collection within the attached property
            // which in turn sets up the attached behavior.
        }
        #endregion

        #region INPC Properties
        private ObservableCollection<MyItem> _myItems;
        public ObservableCollection<MyItem> MyItems
        {
            get => _myItems;
            set => Set(ref _myItems, value);
        }

        private ObservableCollection<MyItem> _mySelectedItems;
        public ObservableCollection<MyItem> MySelectedItems
        {
            get => _mySelectedItems;
            set
            {
                if (MySelectedItems != null) // Remove existing handler if there is already an assignment made (aka the property is not null).
                {
                    MySelectedItems.CollectionChanged -= MySelectedItems_CollectionChanged;
                }

                Set(ref _mySelectedItems, value);

                if (MySelectedItems != null) // Assign the collection changed handler if you need to know when items were added/removed from the collection.
                {
                    MySelectedItems.CollectionChanged += MySelectedItems_CollectionChanged;
                }
            }
        }

        private int _selectionCount;
        public int SelectionCount
        {
            get => _selectionCount;
            set => Set(ref _selectionCount, value);
        }
        #endregion

        #region Private Methods
        private void MySelectedItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // Do whatever you want once the items are added or removed.
            SelectionCount = MySelectedItems != null ? MySelectedItems.Count : 0;
        }
        #endregion

        #region INPC Implementation
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
        #endregion
    }
}
