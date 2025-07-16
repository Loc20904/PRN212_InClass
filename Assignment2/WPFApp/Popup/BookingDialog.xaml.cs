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
    /// Interaction logic for BookingDialog.xaml
    /// </summary>
    public partial class BookingDialog : Window
    {
        public BookingReservation Booking { get; }
        public List<byte> StatusOptions { get; }
        public List<RoomInformation> Rooms { get; }

        public BookingDialog(BookingReservation booking, List<byte> statusOptions)
        {
            
            InitializeComponent();
            Booking = booking;
            StatusOptions = statusOptions;
            Rooms = new RoomInformationDAO().GetAll(); // hoặc Service
            foreach (var detail in Booking.BookingDetails)
            {
                detail.Room = Rooms.FirstOrDefault(r => r.RoomId == detail.RoomId);
            }
            DataContext = this;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Booking.TotalPrice = Booking.BookingDetails.Sum(d => d.ActualPrice);
            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e) => DialogResult = false;
    }


}
