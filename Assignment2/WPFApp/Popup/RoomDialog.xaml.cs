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
using DataAccessLayer;

namespace WPFApp.Popup
{
    /// <summary>
    /// Interaction logic for RoomDialog.xaml
    /// </summary>
    public partial class RoomDialog : Window
    {
        public RoomInformation Room { get; }

        public List<int> StatusOptions => new() { 1, 2 };
        public List<RoomType> RoomTypes { get; } = new RoomTypeDAO().GetAll();


        public RoomDialog(RoomInformation roomToEdit)
        {
            InitializeComponent();
            Room = roomToEdit;
            DataContext = this;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;    // closes dialog & returns true
        }

        private void NumericOnly(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.All(char.IsDigit);
        }
    }
}
