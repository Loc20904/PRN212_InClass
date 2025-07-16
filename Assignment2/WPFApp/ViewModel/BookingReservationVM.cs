using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using BusinessObject;
using Services;
using WPFApp.Popup;
using System.Globalization;
using System.Windows.Data;
using Microsoft.Extensions.Logging;

namespace WPFApp.ViewModel
{
    public class BookingStatusTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return "Unknown";
            int status = System.Convert.ToInt32(value);
            return status switch
            {
                0 => "Pending",
                1 => "Confirmed",
                2 => "Cancelled",
                3 => "Finished",
                _ => "Unknown"
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => Binding.DoNothing;
    }

    public class BookingManagerViewModel : INotifyPropertyChanged
    {
        private readonly BookingReservationService _service = new();
        private readonly ILogger<BookingManagerViewModel> _logger;

        public List<byte> BookingStatusOptions { get; } = new() { 0, 1, 2 };
        public ObservableCollection<BookingReservation> Bookings { get; set; }
        public ICollectionView FilteredBookings { get; }

        private string _keyword = "";
        public string Keyword
        {
            get => _keyword;
            set { _keyword = value; OnPropertyChanged(); FilteredBookings.Refresh(); }
        }

        private BookingReservation? _selectedBooking;
        public BookingReservation? SelectedBooking
        {
            get => _selectedBooking;
            set { _selectedBooking = value; OnPropertyChanged(); }
        }

        public BookingManagerViewModel()
        {
            _logger = App.LoggerFactory.CreateLogger<BookingManagerViewModel>();

            Bookings = new ObservableCollection<BookingReservation>(_service.GetAll());
            _logger.LogInformation("Loaded {Count} bookings", Bookings.Count);

            FilteredBookings = CollectionViewSource.GetDefaultView(Bookings);
            FilteredBookings.Filter = _ => FilterByKeyword(_);
        }

        private bool FilterByKeyword(object item)
        {
            if (item is not BookingReservation b) return false;
            return string.IsNullOrWhiteSpace(Keyword)
                || b.Customer.CustomerFullName.Contains(Keyword, StringComparison.OrdinalIgnoreCase)
                || b.BookingDate.ToString().Contains(Keyword);
        }

        public ICommand CreateCommand => new RelayCommand(_ => OpenDialogForCreate());
        public ICommand EditCommand => new RelayCommand(_ => OpenDialogForEdit(), _ => SelectedBooking != null);
        public ICommand DeleteCommand => new RelayCommand(_ => DeleteSelected(), _ => SelectedBooking != null);
        public ICommand DetailCommand => new RelayCommand(param =>
        {
            if (param is BookingReservation reservation)
            {
                var detail = reservation.BookingDetails?.FirstOrDefault();
                var dlg = new BookingDetailDialog(reservation)
                {
                    Title = $"Booking #{reservation.BookingReservationId} Detail"
                };
                dlg.ShowDialog();
            }
        });

        private void OpenDialogForCreate()
        {

            var fresh = new BookingReservation
            {
                BookingDate = DateOnly.FromDateTime(DateTime.Now),
                BookingStatus = 0,
                CustomerId = 1,
                TotalPrice = 0,
                BookingDetails = new List<BookingDetail>
                {
                    new()
                    {
                        StartDate = DateOnly.FromDateTime(DateTime.Now),
                        EndDate = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
                        RoomId = 1,
                        ActualPrice = 0
                    }
                }
            };

            var dlg = new BookingDialog(fresh, BookingStatusOptions) { Title = "Create Booking" };
            if (dlg.ShowDialog() == true)
            {
                foreach (var detail in fresh.BookingDetails)
                {
                    if (!IsRoomAvailable(detail.RoomId, detail.StartDate, detail.EndDate))
                    {
                        MessageBox.Show($"Room #{detail.RoomId} is already booked during {detail.StartDate} - {detail.EndDate}.",
                                        "Room Unavailable", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }
                _service.Add(fresh);
                Bookings.Add(fresh);
                _logger.LogInformation("Created new booking with CustomerId {CustomerId}", fresh.CustomerId);
            }
        }

        private void OpenDialogForEdit()
        {
            var clone = new BookingReservation
            {
                BookingReservationId = SelectedBooking!.BookingReservationId,
                BookingDate = SelectedBooking.BookingDate,
                BookingStatus = SelectedBooking.BookingStatus,
                CustomerId = SelectedBooking.CustomerId,
                TotalPrice = SelectedBooking.TotalPrice,
                BookingDetails = SelectedBooking.BookingDetails.Select(d => new BookingDetail
                {
                    BookingReservationId = d.BookingReservationId,
                    RoomId = d.RoomId,
                    StartDate = d.StartDate,
                    EndDate = d.EndDate,
                    ActualPrice = d.ActualPrice,
                    Room = d.Room
                }).ToList()
            };

            if (clone.BookingDetails.Count == 0)
            {
                clone.BookingDetails.Add(new BookingDetail
                {
                    StartDate = DateOnly.FromDateTime(DateTime.Now),
                    EndDate = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
                    RoomId = 1,
                    ActualPrice = 0
                });
            }
            foreach (var detail in clone.BookingDetails)
            {
                if (!IsRoomAvailable(detail.RoomId, detail.StartDate, detail.EndDate, clone.BookingReservationId))
                {
                    MessageBox.Show($"Room #{detail.RoomId} is already booked during {detail.StartDate} - {detail.EndDate}.",
                                    "Room Unavailable", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }

            var dlg = new BookingDialog(clone, BookingStatusOptions) { Title = "Update Booking" };
            if (dlg.ShowDialog() == true)
            {
                _service.Update(clone);

                SelectedBooking.BookingDate = clone.BookingDate;
                SelectedBooking.BookingStatus = clone.BookingStatus;
                SelectedBooking.CustomerId = clone.CustomerId;
                SelectedBooking.TotalPrice = clone.TotalPrice;
                SelectedBooking.BookingDetails = clone.BookingDetails;

                FilteredBookings.Refresh();
                _logger.LogInformation("Updated booking #{Id}", SelectedBooking.BookingReservationId);
            }
        }

        private void DeleteSelected()
        {
            if (MessageBox.Show("Are you sure you want to delete this booking?",
                                "Confirm delete", MessageBoxButton.YesNo,
                                MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                _service.Delete(SelectedBooking!.BookingReservationId);
                _logger.LogWarning("Deleted booking #{Id}", SelectedBooking.BookingReservationId);
                Bookings.Remove(SelectedBooking);
            }
        }
        public bool IsRoomAvailable(int roomId, DateOnly newStart, DateOnly newEnd, int? excludeBookingId = null)
        {
            using var context = new FuminiHotelManagementContext();

            var overlaps = context.BookingDetails
                .Where(d => d.RoomId == roomId &&
                           (excludeBookingId == null || d.BookingReservationId != excludeBookingId) &&
                           !(newEnd <= d.StartDate || newStart >= d.EndDate))
                .Any();

            return !overlaps;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string? p = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));
    }

    public class RelayCommand : ICommand
    {
        private readonly Action<object?> _execute;
        private readonly Predicate<object?>? _canExecute;

        public RelayCommand(Action<object?> execute, Predicate<object?>? canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object? parameter) => _canExecute?.Invoke(parameter) ?? true;
        public void Execute(object? parameter) => _execute(parameter);
        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
