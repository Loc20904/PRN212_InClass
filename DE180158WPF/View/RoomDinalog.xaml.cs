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

namespace DE180158WPF.View
{
    public partial class RoomDinalog : Window
    {
        public RoomInformation Room { get; }

        public List<RoomType> RoomTypes => InMemoryDatabase.Instance.RoomTypes;
        public List<int> StatusOptions => new() { 1, 2 };


        public RoomDinalog(RoomInformation roomToEdit)
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
