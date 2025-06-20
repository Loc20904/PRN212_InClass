using System.Windows;
using System.Windows.Controls;
using System.Linq;
using DE180158WPF.DAO;
using DE180158WPF.Models;

namespace DE180158WPF.View
{
    public partial class BookingPage : Page
    {
        private readonly Customer _currentCustomer;
        private readonly BookingDAO _bookingDAO = new BookingDAO();

        public BookingPage(Customer customer)
        {
            var roomDao = new RoomInformationDAO();
            var bookingDao = new BookingDAO();
            InitializeComponent();
            _currentCustomer = customer;

            //var bookedRoomIds = bookingDao.GetAll()
            //.Where(b => b.BookingStatus == 1)
            //.Select(b => b.RoomId)
            //.Distinct()
            //.ToList();

            RoomList.ItemsSource = roomDao.GetAll()
                .Where(r => r.RoomStatus == 1)
                .ToList();
            txtNoRoom.Visibility = RoomList.Items.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
        }

        private void Book_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)?.Tag is not RoomInformation room) return;

            var dialog = new BookingFormWindow(room);
            if (dialog.ShowDialog() != true) return;

            var start = dialog.StartDate;
            var end = dialog.EndDate;

            if (start >= end)
            {
                MessageBox.Show("End date must be after start date.");
                return;
            }
            if (start < DateTime.Today.AddDays(-1))
            {
                MessageBox.Show("Start date can't be before today.");
                return;
            }

            var totalDays = (end - start).Days;
            var total = totalDays * room.RoomPricePerDate;

            var booking = new Booking
            {
                CustomerId = _currentCustomer.CustomerID,
                RoomId = room.RoomID,
                CheckInDate = start,
                CheckOutDate = end,
                TotalPrice = total,
                Customer = _currentCustomer,
                Room = room
            };

            if (_bookingDAO.Add(booking))
            {
                room.RoomStatus = 2;
                new RoomInformationDAO().Update(room);

                MessageBox.Show(
                    $"✅ Booking Successful!\nRoom: {room.RoomNumber}\nFrom: {start:dd/MM} to {end:dd/MM}\nTotal: {total:N0} VND",
                    "Booking", MessageBoxButton.OK, MessageBoxImage.Information);
                RoomList.ItemsSource = new RoomInformationDAO()
                .GetAll();
            }
            else
            {
                MessageBox.Show("❌ Booking failed. Room may already be booked.", "Error");
            }

        }
    }
}
