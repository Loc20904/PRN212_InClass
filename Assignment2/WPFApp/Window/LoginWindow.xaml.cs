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
using System.Windows.Shapes;
using BusinessObject;
using Microsoft.Identity.Client;
using Services;
using Utils;

namespace WPFApp
{
    public static class Session
    {
        public static Customer? CurrentUser { get; set; }
        public static bool IsAdmin => CurrentUser?.EmailAddress
            .Equals(Util.GetConfiguration()["AdminAccount:Username"], StringComparison.OrdinalIgnoreCase) ?? false;
    }
    public partial class LoginWindow : Window
    {
        
        private CustomerService CustomerService { get; } = new CustomerService();
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUser.Text.Trim();
            string password = txtPass.Password.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both email and password.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Customer? user;

            // Check Admin Account
            if (username.Equals(Util.GetConfiguration()["AdminAccount:Username"], StringComparison.OrdinalIgnoreCase))
            {
                if (password != Util.GetConfiguration()["AdminAccount:Password"])
                {
                    MessageBox.Show("Incorrect admin password.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                user = new Customer
                {
                    CustomerId = 0,
                    CustomerFullName = "Admin",
                    EmailAddress = username,
                    Password = password,
                    Telephone = "0000000000",
                    CustomerBirthday = DateOnly.MaxValue,
                    CustomerStatus = 1
                };
            }
            else
            {
                user = CustomerService.GetByEmail(username);
                if (user == null)
                {
                    MessageBox.Show("Email not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (user.Password != password)
                {
                    MessageBox.Show("Incorrect password.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (user.CustomerStatus != 1)
                {
                    MessageBox.Show("Your account is not active.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            Session.CurrentUser = user;
            // Login success
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
        
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
