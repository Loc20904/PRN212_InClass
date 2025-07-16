using BusinessObject;
using DataAccessLayer;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace WPFApp.ViewModel
{
    public class StatisticsViewModel : INotifyPropertyChanged
    {
        private readonly BookingReservationDAO _dao = new();
        
        public DateTime? StartDate { get; set; } = DateTime.Today.AddMonths(-1);
        public DateTime? EndDate { get; set; } = DateTime.Today;

        private string _totalBookingsText = "Total Bookings: ";
        public string TotalBookingsText
        {
            get => _totalBookingsText;
            set { _totalBookingsText = value; OnPropertyChanged(); }
        }

        private string _totalRevenueText = "Total Revenue: ";
        public string TotalRevenueText
        {
            get => _totalRevenueText;
            set { _totalRevenueText = value; OnPropertyChanged(); }
        }

        public ICommand GenerateReportCommand => new RelayCommand(_ => GenerateReport());
        public ObservableCollection<BookingReservation> Bookings { get; } = new();

        private void GenerateReport()
        {
            // Chuyển sang DateOnly để so sánh đúng
            DateOnly? start = StartDate.HasValue ? DateOnly.FromDateTime(StartDate.Value) : null;
            DateOnly? end = EndDate.HasValue ? DateOnly.FromDateTime(EndDate.Value) : null;

            var list = _dao.GetAll()
                           .Where(b =>
                               (!start.HasValue || b.BookingDate >= start) &&
                               (!end.HasValue || b.BookingDate <= end))
                           .OrderByDescending(b => b.BookingDate)
                           .ToList();

            // Cập nhật danh sách
            Bookings.Clear();
            foreach (var b in list)
            {
                Bookings.Add(b);
            }
            // Tổng số & doanh thu
            TotalBookingsText = $"Total Bookings: {list.Count}";
            TotalRevenueText = $"Total Revenue: {list.Sum(b => b.TotalPrice ?? 0):N0} $";
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
