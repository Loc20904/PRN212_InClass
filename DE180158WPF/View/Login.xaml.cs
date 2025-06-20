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
using DE180158WPF.DAO;
using DE180158WPF.Models;
using DE180158WPF.Utils;

namespace DE180158WPF.View
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private readonly static CustomerDAO UserDAO = new CustomerDAO();

        public Login()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            // Handle login button click
            string username = txtEmail.Text;
            string password = txtPassword.Password;
            if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both email and password.","Waring",MessageBoxButton.OK,MessageBoxImage.Warning);
                return;
            }
            Customer user;
            if (username.Equals(AppConfig.Configuration["AdminAccount:Email"]))
            {
                user = new Customer
                {
                    CustomerID = 0,
                    CustomerFullName = "Admin",
                    EmailAddress = username,
                    Password = AppConfig.Configuration["AdminAccount:Password"],
                    Telephone = "0000000000",
                    CustomerBirthday = DateTime.Now,
                    CustomerStatus = 1
                };
            }
            else
            {
                user = UserDAO.GetByEmail(username);
            }
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
            MainWindow mainWindow = new MainWindow(user);
            mainWindow.Show();
            this.Close();
        }
        
    }
}
