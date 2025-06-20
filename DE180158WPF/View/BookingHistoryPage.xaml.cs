using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DE180158WPF.DAO;
using DE180158WPF.Models;

namespace DE180158WPF.View
{
   
    public partial class BookingHistoryPage : Page
    {
        private Customer? _currentCus = null!;
        public BookingHistoryPage(Customer customer)
        {
            InitializeComponent();
            _currentCus = customer;
            var bookings = new BookingDAO()
                .GetAll()
                .Where(b => b.CustomerId == customer.CustomerID)
                .ToList();

            BookingGrid.ItemsSource = bookings;
            
        }

    }
}
