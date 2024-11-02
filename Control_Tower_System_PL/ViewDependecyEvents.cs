using System.Windows;
using System.Windows.Controls;

namespace Control_Tower_System_PL
{
    public class ViewDependecyEvents
    {
        #region event for getting selected items from ListView
        public static bool GetSelectedItemsListViewEvents(DependencyObject obj)
        {
            return (bool)obj.GetValue(EnableSelectedItemsListViewProperty);
        }

        public static void SetSelectedItemsListViewEvents(DependencyObject obj, bool value)
        {
            obj.SetValue(EnableSelectedItemsListViewProperty, value);
        }

        public static readonly DependencyProperty EnableSelectedItemsListViewProperty = DependencyProperty.RegisterAttached("EnableSelectedItemsListViewEvents", typeof(bool), typeof(EventManager), new PropertyMetadata(false, EnableSelectedItemsListViewChanged));

        private static void EnableSelectedItemsListViewChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ListView listView)
            {
                listView.SelectionChanged += (s, e) =>
                {
                    if (listView.DataContext is ViewModel vm)
                    {
                        if (listView.Name == "FlightInformation")
                        {
                            vm.CurrentSelectedFlight = (Control_Tower_System_DTO.Flight)listView.SelectedItem;
                        }
                    }
                };
            }
        }
    }
    #endregion
}

