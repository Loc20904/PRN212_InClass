using System.Windows;
using BusinessObject;

namespace WPFApp.Popup
{
    /// <summary>
    /// Interaction logic for BookingDetailDialog.xaml
    /// </summary>
    public partial class BookingDetailDialog : Window
    {
        public BookingReservation Booking { get; }
        //public BookingDetail? Detail { get; }

        public BookingDetailDialog(BookingReservation booking)
        {
            InitializeComponent();
            Booking = booking;
            DataContext = this;
            
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if(Session.IsAdmin)
                Close();
            else
            {
                this.DialogResult = true;
                Close();
            }
        }
    }
}
