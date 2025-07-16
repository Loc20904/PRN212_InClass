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

namespace WPFApp.Page
{
    /// <summary>
    /// Interaction logic for ManagerPage.xaml
    /// </summary>
    public partial class ManagerPage : System.Windows.Controls.Page
    {
        public ManagerPage()
        {
            InitializeComponent();
        }
        private void Room_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Uri("Page/RoomManagerPage.xaml", UriKind.Relative));
        }
        private void Customer_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Uri("Page/CustomerManagePage.xaml", UriKind.Relative));
        }
        private void Booking_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Uri("Page/BookingPage.xaml", UriKind.Relative));
        }
        private void ManageRType_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Uri("Page/RoomTypeManagePage.xaml", UriKind.Relative));
        }
    }
}
