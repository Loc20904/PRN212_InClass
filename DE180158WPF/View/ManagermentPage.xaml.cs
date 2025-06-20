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

namespace DE180158WPF.View
{
    /// <summary>
    /// Interaction logic for ManagermentPage.xaml
    /// </summary>
    public partial class ManagermentPage : Page
    {
        public ManagermentPage()
        {
            InitializeComponent();
        }

        private void Room_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Uri("View/RoomManagerPage.xaml", UriKind.Relative));
        }
        private void Customer_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Uri("View/CustomerManagePage.xaml", UriKind.Relative));
        }
        private void Booking_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Uri("View/BookingManagePage.xaml", UriKind.Relative));
        }
        private void ManageRType_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Uri("View/RoomTypeManagePage.xaml", UriKind.Relative));
        }
    }
}
