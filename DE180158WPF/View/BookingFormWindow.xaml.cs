using System;
using System.Windows;
using System.Windows.Media;
using DE180158WPF.Models;

namespace DE180158WPF.View
{
    public partial class BookingFormWindow : Window
    {
        public RoomInformation Room { get; }
        public DateTime StartDate => dpStart.SelectedDate ?? DateTime.Today;
        public DateTime EndDate => dpEnd.SelectedDate ?? DateTime.Today;

        public BookingFormWindow(RoomInformation room)
        {
            InitializeComponent();
            Room = room;
            DataContext = this;

            dpStart.SelectedDate = DateTime.Today;
            dpEnd.SelectedDate = DateTime.Today.AddDays(1);
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            if (!dpStart.SelectedDate.HasValue || !dpEnd.SelectedDate.HasValue)
            {
                MessageBox.Show("Please select both dates.");
                return;
            }

            DialogResult = true;
        }
    }
}
