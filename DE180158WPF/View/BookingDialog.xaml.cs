using System.Collections.Generic;
using System.Windows;
using DE180158WPF.DAO;
using DE180158WPF.Models;

namespace DE180158WPF.View
{
    public partial class BookingDialog : Window
    {
        public Booking Booking { get; }
        public List<Customer> Customers => InMemoryDatabase.Instance.Customers;
        public List<RoomInformation> Rooms => InMemoryDatabase.Instance.Rooms;

        public BookingDialog(Booking bookingToEdit)
        {
            InitializeComponent();
            Booking = bookingToEdit;
            DataContext = this;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true; // Closes dialog & returns true
        }
    }
}