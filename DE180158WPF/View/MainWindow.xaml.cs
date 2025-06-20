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
using DE180158WPF.DAO;
using DE180158WPF.Models;
using DE180158WPF.Utils;

namespace DE180158WPF.View
{
    public partial class MainWindow : Window
    {
        private Customer? _currentCus = null!;
        private bool isAdmin = false;
        public MainWindow(Models.Customer user)
        {
            InitializeComponent();
            _currentCus = user;

            lblWelcome.Content = "Welcome " + _currentCus.CustomerFullName;

            isAdmin = _currentCus.EmailAddress
                           .Equals(AppConfig.Configuration["AdminAccount:Email"],
                                   StringComparison.OrdinalIgnoreCase);

            // nút Quản lý chỉ để Admin nhìn thấy
            btnManagerment.Visibility = isAdmin ? Visibility.Visible : Visibility.Collapsed;

            // nút Xem lịch sử chỉ để Customer nhìn thấy
            btnBHistory.Visibility = isAdmin ? Visibility.Collapsed : Visibility.Visible;

            // Chỉ khách mới được load BookingPage
            if (!isAdmin)
            {
                MainFrame.Navigate(new BookingPage(_currentCus));
            }
            else
            {
                MainFrame.Navigate(new StatisticPage());
            }
        }

        private void btnManagerment_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ManagermentPage());
        }

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            CustomerDAO _dao = new CustomerDAO();
            var clone = new Customer
            {
                CustomerID = _currentCus!.CustomerID,
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
                _dao.Update(clone);
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
                new Login().Show();
                this.Close();
            }
        }

        private void btnBHistory_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new BookingHistoryPage(_currentCus!));
        }
        private void btnHP_Click(object sender, RoutedEventArgs e)
        {
            if (isAdmin)
                MainFrame.Navigate(new StatisticPage());
            else
                MainFrame.Navigate(new BookingPage(_currentCus!));
            
        }

    }
}