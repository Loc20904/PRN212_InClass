using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using DE180158WPF.DAO;
using DE180158WPF.Models;

namespace DE180158WPF.ViewModel
{
    public class StatisticsViewModel : INotifyPropertyChanged
    {
        private readonly BookingDAO _dao = new();

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

        public ICommand GenerateReportCommand => new RelayCmd(_ => GenerateReport());
        public ObservableCollection<Booking> Bookings { get; } = new();

        private void GenerateReport()
        {
            var list = _dao.Search("", StartDate, EndDate)
                           .OrderByDescending(b => b.CheckInDate)
                           .ToList();

            // Cập nhật danh sách
            Bookings.Clear();
            foreach (var b in list) Bookings.Add(b);

            // Tổng số & doanh thu
            TotalBookingsText = $"Total Bookings: {list.Count}";
            TotalRevenueText = $"Total Revenue: {list.Sum(b => b.TotalPrice):N0} VND";
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
