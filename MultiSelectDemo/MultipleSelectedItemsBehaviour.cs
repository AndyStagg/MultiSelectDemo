using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace MultiSelectDemo
{
    /// <summary>
    /// Provides a bindable implementation of the DataGrid.SelectedItems property.
    /// </summary>
    public class MultipleSelectedItemsBehaviour
    {
        /// <summary>
        /// Configures the bindable MultipleSelectedItems Attached Property/Behaviour.
        /// </summary>
        /// <remarks>
        /// The type of this property must IList and not IEnumerable as items need to added or removed from it.
        /// </remarks>
        public static readonly DependencyProperty MultipleSelectedItemsProperty =
            DependencyProperty.RegisterAttached("MultipleSelectedItems", typeof(IList), typeof(MultipleSelectedItemsBehaviour),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, MultipleSelectedItemsChangedCallback));

        public static IList GetMultipleSelectedItems(DependencyObject d) => (IList)d.GetValue(MultipleSelectedItemsProperty);
        public static void SetMultipleSelectedItems(DependencyObject d, IList value) => d.SetValue(MultipleSelectedItemsProperty, value);

        /// <summary>
        /// Adds or removes the SelectedChanged EventHandler based on whether or not the new value is null.
        /// </summary>
        public static void MultipleSelectedItemsChangedCallback(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is DataGrid dataGrid)
            {
                if (e.NewValue == null)
                {
                    dataGrid.SelectionChanged -= DataGrid_SelectionChanged;
                }
                else
                {
                    dataGrid.SelectionChanged += DataGrid_SelectionChanged;
                }
            }
        }

        /// <summary>
        /// Handles the SelectedChanged Event.
        /// </summary>
        /// <remarks>
        /// Directly setting the MultipleSelectedItems property to the dataGrid.SelectedItems property results<br/>
        /// in a null conversion result. This method instead adds/removes the items from the collection, which<br/>
        /// should be initialized and set within the ViewModel.
        /// </remarks>
        private static void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is DataGrid dataGrid)
            {
                var selectedItems = GetMultipleSelectedItems(dataGrid);

                if (selectedItems == null) return;

                foreach (var item in e.AddedItems)
                {
                    try
                    {
                        selectedItems.Add(item);
                    }
                    catch (ArgumentException)
                    {
                        // The "NewItemPlaceHolder" will cause an exception to be thrown.
                        // I am ignore it here, but you may need to address this if it's
                        // a common case for you.
                    }
                }

                foreach (var item in e.RemovedItems)
                {
                    selectedItems.Remove(item);
                }
            }
        }
    }
}
