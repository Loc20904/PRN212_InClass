using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BusinessObject;
using WPFApp.ViewModel;
using System.Windows.Controls.Primitives;

namespace WPFApp.Page
{
    /// <summary>
    /// Interaction logic for AvailableRoomPage.xaml
    /// </summary>
    public partial class AvailableRoomPage : System.Windows.Controls.Page
    {
        public AvailableRoomPage()
        {
            InitializeComponent();
            this.DataContext = new AvailableRoomViewModel();
        }
        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            var grid = sender as DataGrid;
            if (grid == null) return;

            foreach (var item in grid.Items)
            {
                var row = (DataGridRow)grid.ItemContainerGenerator.ContainerFromItem(item);
                if (row == null) continue;

                foreach (var datePicker in FindVisualChildren<DatePicker>(row))
                {
                    if (datePicker.DataContext is RoomBookingItem roomItem)
                    {
                        datePicker.BlackoutDates.Clear();

                        foreach (var range in roomItem.BlackoutDateRanges)
                        {
                            try
                            {
                                if (range != null && range.Start <= range.End)
                                {
                                    datePicker.BlackoutDates.Add(range);
                                }
                            }
                            catch (ArgumentOutOfRangeException ex)
                            {
                                // Ghi log nếu cần
                                Console.WriteLine($"Invalid blackout range: {range.Start} - {range.End}");
                            }
                        }
                    }
                }
            }
        }

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj == null) yield break;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                var child = VisualTreeHelper.GetChild(depObj, i);
                if (child is T t) yield return t;

                foreach (var childOfChild in FindVisualChildren<T>(child))
                    yield return childOfChild;
            }
        }

    }

}
