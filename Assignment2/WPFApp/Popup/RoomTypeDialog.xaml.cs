using System.Windows;
using System.Windows.Input;
using BusinessObject;

namespace WPFApp.Popup
{
    public partial class RoomTypeDialog : Window
    {
        public RoomType RoomType { get; }

        public RoomTypeDialog(RoomType roomType)
        {
            InitializeComponent();
            RoomType = roomType;
            DataContext = this;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
        private void TextOnly(object sender, TextCompositionEventArgs e)
        {
            // Chỉ cho phép chữ cái và khoảng trắng
            e.Handled = !e.Text.All(c => char.IsLetter(c) || char.IsWhiteSpace(c));
        }
    }
}