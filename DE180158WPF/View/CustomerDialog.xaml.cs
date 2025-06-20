using System.Windows;
using System.Windows.Input;
using DE180158WPF.Models;

namespace DE180158WPF.View
{
    public partial class CustomerDialog : Window
    {
        public Customer Customer { get; }
        public List<int> StatusOptions => new() { 1, 2 }; // 1: Active, 2: Deleted

        public CustomerDialog(Customer customer)
        {
            InitializeComponent();
            Customer = customer;
            DataContext = this;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            string finalPassword = PasswordBoxHidden.Visibility == Visibility.Visible
           ? PasswordBoxHidden.Password
           : PasswordBoxVisible.Text;
            Customer.Password = finalPassword;
            if (Customer.CustomerBirthday == null)
            {
                MessageBox.Show("Please select a birth date.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var today = DateTime.Today;
            var age = today.Year - Customer.CustomerBirthday.Year;
            if (Customer.CustomerBirthday > today.AddYears(-age)) age--; // kiểm tra ngày chưa đến sinh nhật

            if (age < 18)
            {
                MessageBox.Show("Customer must be at least 18 years old.", "Age Restriction", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DialogResult = true;
        }

        private void ShowPassword_Checked(object sender, RoutedEventArgs e)
        {
            PasswordBoxVisible.Text = PasswordBoxHidden.Password;
            PasswordBoxVisible.Visibility = Visibility.Visible;
            PasswordBoxHidden.Visibility = Visibility.Collapsed;
        }

        private void ShowPassword_Unchecked(object sender, RoutedEventArgs e)
        {
            PasswordBoxHidden.Password = PasswordBoxVisible.Text;
            PasswordBoxHidden.Visibility = Visibility.Visible;
            PasswordBoxVisible.Visibility = Visibility.Collapsed;
        }
        private void NumericOnly(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.All(char.IsDigit);
        }
        private void TextOnly(object sender, TextCompositionEventArgs e)
        {
            // Chỉ cho phép chữ cái và khoảng trắng
            e.Handled = !e.Text.All(c => char.IsLetter(c) || char.IsWhiteSpace(c));
        }

    }
}