using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BusinessObject;
using DataAccessLayer;
using WPFApp.Popup;
using WPFApp.ViewModel;

namespace WPFApp.Page
{
    public partial class BookingHistoryPage : System.Windows.Controls.Page
    {
        public ObservableCollection<BookingReservation> Reservations { get; set; }
        public ICommand ViewDetailCommand { get; }

        public BookingHistoryPage(Customer customer)
        {
            InitializeComponent();

            var list = new BookingReservationDAO()
                .GetAll()
                .Where(r => r.CustomerId == customer.CustomerId)
                .ToList();

            Reservations = new ObservableCollection<BookingReservation>(list);
            ViewDetailCommand = new RelayCommand(OpenDetailDialog);

            this.DataContext = this;
        }

        private void OpenDetailDialog(object? parameter)
        {
            if (parameter is BookingReservation booking)
            {
                var dlg = new BookingDetailDialog(booking)
                {
                    Title = $"Booking #{booking.BookingReservationId} Details"
                };
                dlg.ShowDialog();
            }
        }
    }
}
