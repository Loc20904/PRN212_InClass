using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace WPFApp.Popup
{
    public class DateOnlyToDateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateOnly dateOnly)
                return dateOnly.ToDateTime(TimeOnly.MinValue);
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime dateTime)
                return DateOnly.FromDateTime(dateTime);
            return null;
        }
    }
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

            var today = DateOnly.FromDateTime(DateTime.Today); // chuyển DateTime -> DateOnly
            var birthDate = Customer.CustomerBirthday!.Value;

            var age = today.Year - birthDate.Year;

            // Nếu hôm nay trước ngày sinh nhật trong năm nay → chưa đủ tuổi
            if (today < new DateOnly(today.Year, birthDate.Month, birthDate.Day))
            {
                age--;
            }

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
