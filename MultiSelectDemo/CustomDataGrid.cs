using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace MultiSelectDemo
{
    public class CustomDataGrid : DataGrid
    {
        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);

            MultiSelectedItems = this.SelectedItems;
        }

        public readonly static DependencyProperty MultiSelectedItemsProperty =
            DependencyProperty.Register(nameof(MultiSelectedItems), typeof(IList), typeof(CustomDataGrid),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public IList MultiSelectedItems
        {
            get => (IList)GetValue(MultiSelectedItemsProperty);
            set => SetValue(MultiSelectedItemsProperty, value);
        }
    }
}
