using System.Text;
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
using DataAccessLayer;
using Microsoft.Identity.Client;
using Utils;
using WPFApp.Page;
using WPFApp.Popup;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Customer? _currentCus = Session.CurrentUser;
        private readonly bool isAdmin;
        public MainWindow()
        {
            InitializeComponent();
            lblWelcome.Content = "Welcome " + _currentCus.CustomerFullName;
            isAdmin = _currentCus.EmailAddress
                           .Equals(Util.GetConfiguration()["AdminAccount:Username"],
                                   StringComparison.OrdinalIgnoreCase);

            // nút Quản lý chỉ để Admin nhìn thấy
            btnManagerment.Visibility = isAdmin ? Visibility.Visible : Visibility.Collapsed;

            // nút Xem lịch sử chỉ để Customer nhìn thấy
            btnBHistory.Visibility = isAdmin ? Visibility.Collapsed : Visibility.Visible;

            //Chỉ khách mới được load BookingPage
            if (!isAdmin)
            {
                MainFrame.Navigate(new AvailableRoomPage());
            }
            else
            {
                MainFrame.Navigate(new StatisticPage());
            }
        }

        private void btnManagerment_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ManagerPage());
        }

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            CustomerDAO _dao = new CustomerDAO();
            var clone = new Customer
            {
                CustomerId = _currentCus!.CustomerId,
                CustomerFullName = _currentCus.CustomerFullName,
                Telephone = _currentCus.Telephone,
                EmailAddress = _currentCus.EmailAddress,
                CustomerBirthday = _currentCus.CustomerBirthday,
                CustomerStatus = _currentCus.CustomerStatus,
                Password = _currentCus.Password
            };

            var dlg = new CustomerDialog(clone) { Title = "Customer Profile" };
            if (dlg.ShowDialog() == true)
            {
                _dao.UpdateCustomer(clone);
            }
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to logout?",
                                         "Confirm Logout",
                                         MessageBoxButton.YesNo,
                                         MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                new LoginWindow().Show();
                this.Close();
            }
        }

        private void btnBHistory_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new BookingHistoryPage(Session.CurrentUser));
        }
        private void btnHP_Click(object sender, RoutedEventArgs e)
        {
            if (isAdmin)
                MainFrame.Navigate(new StatisticPage());
            else
                MainFrame.Navigate(new AvailableRoomPage());

        }
    }
}