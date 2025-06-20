using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using DE180158WPF.DAO;
using DE180158WPF.Models;
using DE180158WPF.View;

namespace DE180158WPF.ViewModel
{
    public class BookingManagerViewModel : INotifyPropertyChanged
    {
        private readonly BookingDAO _service = new BookingDAO();
        private ObservableCollection<Booking> _bookings;
        private Booking? _selectedBooking;
        private string _customerNameKeyword = "";
        public string CustomerNameKeyword
        {
            get => _customerNameKeyword;
            set
            {
                _customerNameKeyword = value;
                OnPropertyChanged();
                FilteredBookings.Refresh();
            }
        }

        private DateTime? _fromDate;
        private DateTime? _toDate;

        public ObservableCollection<Booking> Bookings
        {
            get => _bookings;
            set
            {
                _bookings = value;
                OnPropertyChanged();
            }
        }

        public ICollectionView FilteredBookings { get; }

        public Booking? SelectedBooking
        {
            get => _selectedBooking;
            set
            {
                _selectedBooking = value;
                OnPropertyChanged();
            }
        }

        

        public DateTime? FromDate
        {
            get => _fromDate;
            set
            {
                _fromDate = value;
                OnPropertyChanged();
                FilteredBookings.Refresh();
            }
        }

        public DateTime? ToDate
        {
            get => _toDate;
            set
            {
                _toDate = value;
                OnPropertyChanged();
                FilteredBookings.Refresh();
            }
        }

        public BookingManagerViewModel()
        {
            Bookings = new ObservableCollection<Booking>(_service.GetAll());
            FilteredBookings = CollectionViewSource.GetDefaultView(Bookings);
            FilteredBookings.Filter = _ => FilterBookings(_);
        }

        private bool FilterBookings(object item)
        {
            if (item is not Booking booking) return false;

            bool matchesCustomer = string.IsNullOrWhiteSpace(CustomerNameKeyword) ||
                (booking.Customer != null &&
                 booking.Customer.CustomerFullName.Contains(CustomerNameKeyword, StringComparison.OrdinalIgnoreCase));

            bool matchesFromDate = !FromDate.HasValue || booking.CheckInDate >= FromDate.Value;
            bool matchesToDate = !ToDate.HasValue || booking.CheckOutDate <= ToDate.Value;

            return matchesCustomer && matchesFromDate && matchesToDate;
        }


        // Commands
        public ICommand SearchCommand => new RelayCmd(_ => FilteredBookings.Refresh());
        public ICommand CreateCommand => new RelayCmd(_ => OpenDialogForCreate());
        public ICommand EditCommand => new RelayCmd(_ => OpenDialogForEdit(), _ => SelectedBooking != null);
        public ICommand DeleteCommand => new RelayCmd(_ => DeleteSelected(), _ => SelectedBooking != null);

        private void OpenDialogForCreate()
        {
            var fresh = new Booking
            {
                BookingStatus = 1, // Active
                CheckInDate = DateTime.Today,
                CheckOutDate = DateTime.Today.AddDays(1)
            };
            var dlg = new BookingDialog(fresh) { Title = "Create Booking" };
            if (dlg.ShowDialog() == true)
            {
                decimal totalPrice = _service.CalculateTotalPrice(fresh.RoomId, fresh.CheckInDate, fresh.CheckOutDate);
                fresh.TotalPrice = totalPrice;
                if (_service.Add(fresh))
                {
                    Bookings.Add(fresh);
                    FilteredBookings.Refresh();
                }
                else
                {
                    MessageBox.Show("Failed to create booking. Please check the input data.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void OpenDialogForEdit()
        {
            var clone = new Booking
            {
                Id = SelectedBooking!.Id,
                CustomerId = SelectedBooking.CustomerId,
                RoomId = SelectedBooking.RoomId,
                CheckInDate = SelectedBooking.CheckInDate,
                CheckOutDate = SelectedBooking.CheckOutDate,
                TotalPrice = SelectedBooking.TotalPrice,
                BookingStatus = SelectedBooking.BookingStatus
            };

            var dlg = new BookingDialog(clone) { Title = "Update Booking" };
            if (dlg.ShowDialog() == true)
            {
                decimal totalPrice = _service.CalculateTotalPrice(clone.RoomId, clone.CheckInDate, clone.CheckOutDate);
                clone.TotalPrice = totalPrice;
                if (_service.Update(clone))
                {
                    SelectedBooking.CustomerId = clone.CustomerId;
                    SelectedBooking.RoomId = clone.RoomId;
                    SelectedBooking.CheckInDate = clone.CheckInDate;
                    SelectedBooking.CheckOutDate = clone.CheckOutDate;
                    SelectedBooking.TotalPrice = clone.TotalPrice;
                    SelectedBooking.BookingStatus = clone.BookingStatus;
                    SelectedBooking.Customer = InMemoryDatabase.Instance.Customers.FirstOrDefault(c => c.CustomerID == clone.CustomerId);
                    SelectedBooking.Room = InMemoryDatabase.Instance.Rooms.FirstOrDefault(r => r.RoomID == clone.RoomId);
                    FilteredBookings.Refresh();
                }
                else
                {
                    MessageBox.Show("Failed to update booking. Please check the input data.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void DeleteSelected()
        {
            if (MessageBox.Show("Are you sure you want to cancel this booking?", "Confirm Cancel", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                if (_service.Delete(SelectedBooking!.Id))
                {
                    Bookings.Remove(SelectedBooking);
                    FilteredBookings.Refresh();
                }
                else
                {
                    MessageBox.Show("Failed to cancel booking.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        // INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}