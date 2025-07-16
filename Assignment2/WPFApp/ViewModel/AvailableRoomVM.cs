using BusinessObject;
using DataAccessLayer;
using Microsoft.Extensions.Logging;
using Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using WPFApp.Popup;

namespace WPFApp.ViewModel
{
    // Converter dùng chung
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => (value is bool b && b) ? Visibility.Visible : Visibility.Collapsed;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

    public class DateOnlyToDateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateOnly dateOnly)
            {
                return dateOnly.ToDateTime(TimeOnly.MinValue);
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime dateTime)
            {
                return DateOnly.FromDateTime(dateTime);
            }
            return null;
        }
    }

    public class RoomBookingItem : INotifyPropertyChanged
    {
        public ObservableCollection<CalendarDateRange> BlackoutDateRanges { get; set; } = new();
        public List<(DateOnly Start, DateOnly End)> BookedRanges { get; set; } = new();

        public RoomInformation Room { get; set; }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set { _isSelected = value; OnPropertyChanged(); }
        }

        private DateOnly? _startDate;
        public DateOnly? StartDate
        {
            get => _startDate;
            set { _startDate = value; OnPropertyChanged(); }
        }

        private DateOnly? _endDate;
        public DateOnly? EndDate
        {
            get => _endDate;
            set { _endDate = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    public static class VisualTreeHelperExtensions
    {
        public static IEnumerable<T> FindVisualChildren<T>(this DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    var child = VisualTreeHelper.GetChild(depObj, i);
                    if (child is T t) yield return t;
                    foreach (var childOfChild in FindVisualChildren<T>(child)) yield return childOfChild;
                }
            }
        }
    }
    public class DateRange
    {
        public DateOnly Start { get; set; }
        public DateOnly End { get; set; }
    }

    public class AvailableRoomViewModel : INotifyPropertyChanged
    {
        private readonly RoomInformationDAO _roomService = new();
        private readonly BookingReservationService _bookingService = new();
        private readonly ILogger<AvailableRoomViewModel> _logger= App.LoggerFactory.CreateLogger<AvailableRoomViewModel>();

        public ObservableCollection<RoomBookingItem> Rooms { get; set; } = new();
        public List<byte> StatusOptions { get; } = new() { 0, 1, 2 };

        private DateOnly _filterStartDate = DateOnly.FromDateTime(DateTime.Today);
        public DateOnly FilterStartDate
        {
            get => _filterStartDate;
            set { _filterStartDate = value; OnPropertyChanged(); }
        }

        private DateOnly _filterEndDate = DateOnly.FromDateTime(DateTime.Today.AddDays(1));
        public DateOnly FilterEndDate
        {
            get => _filterEndDate;
            set { _filterEndDate = value; OnPropertyChanged(); }
        }

        public AvailableRoomViewModel() => load();
        public List<DateRange> GetBookedRanges(int roomId)
        {
            using var context = new FuminiHotelManagementContext();
            return context.BookingDetails
                .Where(d => d.RoomId == roomId)
                .Select(d => new DateRange
                {
                    Start = d.StartDate,
                    End = d.EndDate
                })
                .ToList();
        }

        public void load()
        {
            var availableRooms = _roomService.GetAvailableRooms();
            Rooms.Clear();

            foreach (var r in availableRooms)
            {
                var booked = GetBookedRanges(r.RoomId);
                var blackoutRanges = new ObservableCollection<CalendarDateRange>(
                    booked.Select(b => new CalendarDateRange(
                        b.Start.ToDateTime(TimeOnly.MinValue),
                        b.End.ToDateTime(TimeOnly.MinValue).AddDays(-1)))
                );

                Rooms.Add(new RoomBookingItem
                {
                    Room = r,
                    IsSelected = false,
                    StartDate = FilterStartDate,
                    EndDate = FilterEndDate,
                    BookedRanges = booked.Select(b => (b.Start, b.End)).ToList(),
                    BlackoutDateRanges = blackoutRanges
                });
            }
        }


        public ICommand FilterCommand => new RelayCommand(_ => Filter());
        public ICommand BookCommand => new RelayCommand(_ => BookSelectedRooms(), _ => Rooms.Any(r => r.IsSelected));

        private void Filter()
        {
            var availableRooms = _roomService.GetAvailableRooms(FilterStartDate, FilterEndDate);
            Rooms.Clear();

            foreach (var r in availableRooms)
            {
                var booked = GetBookedRanges(r.RoomId);
                var blackoutRanges = new ObservableCollection<CalendarDateRange>(
                    booked.Select(b => new CalendarDateRange(
                        b.Start.ToDateTime(TimeOnly.MinValue),
                        b.End.ToDateTime(TimeOnly.MinValue).AddDays(-1)))
                );

                Rooms.Add(new RoomBookingItem
                {
                    Room = r,
                    IsSelected = false,
                    StartDate = FilterStartDate,
                    EndDate = FilterEndDate,
                    BookedRanges = booked.Select(b => (b.Start, b.End)).ToList(),
                    BlackoutDateRanges = blackoutRanges
                });
            }
        }


        public bool IsRoomAvailable(int roomId, DateOnly newStart, DateOnly newEnd, int? excludeBookingId = null)
        {
            using var context = new FuminiHotelManagementContext();
            return !context.BookingDetails.Any(d => d.RoomId == roomId && (excludeBookingId == null || d.BookingReservationId != excludeBookingId) && !(newEnd <= d.StartDate || newStart >= d.EndDate));
        }

        private void BookSelectedRooms()
        {
            var dataGrid = Application.Current.Windows.OfType<Window>().SelectMany(w => w.FindVisualChildren<DataGrid>()).FirstOrDefault();
            dataGrid?.CommitEdit();

            var selected = Rooms.Where(r => r.IsSelected && r.StartDate.HasValue && r.EndDate.HasValue).ToList();
            if (selected.Count == 0)
            {
                MessageBox.Show("Please select rooms and enter dates.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            foreach (var item in selected)
            {
                if (item.EndDate <= item.StartDate)
                {
                    MessageBox.Show($"End date must be after start date for room {item.Room.RoomNumber}.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            // Lấy ID thủ công
            int newId;
            using (var context = new FuminiHotelManagementContext())
            {
                newId = (context.BookingReservations.Max(b => (int?)b.BookingReservationId) ?? 0) + 1;
            }

            var booking = new BookingReservation
            {
                BookingReservationId = newId,
                CustomerId = Session.CurrentUser.CustomerId,
                Customer = Session.CurrentUser, // 👈 Gán để hiển thị tên
                BookingDate = DateOnly.FromDateTime(DateTime.Today),
                BookingStatus = 0,
                TotalPrice = 0,
                BookingDetails = new List<BookingDetail>()
            };

            foreach (var item in selected)
            {
                var nights = item.EndDate.Value.DayNumber - item.StartDate.Value.DayNumber;
                if (nights <= 0)
                {
                    MessageBox.Show($"Invalid date for room {item.Room.RoomNumber}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!IsRoomAvailable(item.Room.RoomId, item.StartDate.Value, item.EndDate.Value))
                {
                    MessageBox.Show($"❌ Room #{item.Room.RoomNumber} is already booked from {item.StartDate} to {item.EndDate}.", "Room Unavailable", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var price = (item.Room.RoomPricePerDay ?? 0) * nights;
                booking.TotalPrice += price;

                booking.BookingDetails.Add(new BookingDetail
                {
                    BookingReservationId = newId,
                    RoomId = item.Room.RoomId,
                    Room = item.Room, // 👈 Gán Room để binding RoomNumber
                    StartDate = item.StartDate.Value,
                    EndDate = item.EndDate.Value,
                    ActualPrice = price
                });
            }

            var dlg = new BookingDetailDialog(booking) { Title = "Confirm Booking" };
            if (dlg.ShowDialog() == true)
            {
                // Xoá các navigation property để tránh EF tracking
                booking.Customer = null;
                foreach (var d in booking.BookingDetails)
                    d.Room = null;

                _bookingService.Add(booking);
                MessageBox.Show("✅ Booking successful.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }



        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string? p = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));
    }
}
