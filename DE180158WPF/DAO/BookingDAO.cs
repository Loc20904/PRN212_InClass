using System;
using System.Collections.Generic;
using System.Linq;
using DE180158WPF.Models;

namespace DE180158WPF.DAO
{
    public class BookingDAO
    {
        private readonly InMemoryDatabase _db = InMemoryDatabase.Instance;

        public List<Booking> GetAll()
        {
            var db = InMemoryDatabase.Instance;

            var bookings = db.Bookings
                .ToList();

            foreach (var booking in bookings)
            {
                booking.Room = db.Rooms.FirstOrDefault(r => r.RoomID == booking.RoomId);
                booking.Customer = db.Customers.FirstOrDefault(c => c.CustomerID == booking.CustomerId);
            }

            return bookings;
        }


        public Booking? GetById(int id) =>
            _db.Bookings.FirstOrDefault(b => b.Id == id);

        public bool Add(Booking booking)
        {
            // Kiểm tra dữ liệu đầu vào
            if (booking == null || booking.CheckInDate >= booking.CheckOutDate || booking.TotalPrice < 0)
            {
                return false;
            }

            // Kiểm tra phòng có tồn tại và đang hoạt động
            var room = _db.Rooms.FirstOrDefault(r => r.RoomID == booking.RoomId && r.RoomStatus == 1);
            if (room == null)
            {
                return false;
            }

            // Kiểm tra khách hàng có tồn tại
            var customer = _db.Customers.FirstOrDefault(c => c.CustomerID == booking.CustomerId && c.CustomerStatus == 1);
            if (customer == null)
            {
                return false;
            }

            // Kiểm tra xung đột thời gian
            if (_db.Bookings.Any(b => b.RoomId == booking.RoomId &&
                                     b.BookingStatus == 1 &&
                                     ((booking.CheckInDate >= b.CheckInDate && booking.CheckInDate < b.CheckOutDate) ||
                                      (booking.CheckOutDate > b.CheckInDate && booking.CheckOutDate <= b.CheckOutDate) ||
                                      (booking.CheckInDate <= b.CheckInDate && booking.CheckOutDate >= b.CheckOutDate))))
            {
                return false; // Phòng đã được đặt
            }

            booking.Id = _db.NextBookingId();
            booking.BookingStatus = 1; // Mặc định là Active
            _db.Bookings.Add(booking);
            return true;
        }

        public bool Update(Booking booking)
        {
            if (booking == null || booking.CheckInDate >= booking.CheckOutDate || booking.TotalPrice < 0)
            {
                return false;
            }

            var existing = GetById(booking.Id);
            if (existing == null)
            {
                return false;
            }

            // Kiểm tra phòng có tồn tại và đang hoạt động
            var room = _db.Rooms.FirstOrDefault(r => r.RoomID == booking.RoomId && r.RoomStatus == 1);
            if (room == null)
            {
                return false;
            }

            // Kiểm tra khách hàng có tồn tại
            var customer = _db.Customers.FirstOrDefault(c => c.CustomerID == booking.CustomerId && c.CustomerStatus == 1);
            if (customer == null)
            {
                return false;
            }

            // Kiểm tra xung đột thời gian (trừ booking hiện tại)
            if (_db.Bookings.Any(b => b.Id != booking.Id &&
                                     b.RoomId == booking.RoomId &&
                                     b.BookingStatus == 1 &&
                                     ((booking.CheckInDate >= b.CheckInDate && booking.CheckInDate < b.CheckOutDate) ||
                                      (booking.CheckOutDate > b.CheckInDate && booking.CheckOutDate <= b.CheckOutDate) ||
                                      (booking.CheckInDate <= b.CheckInDate && booking.CheckOutDate >= b.CheckOutDate))))
            {
                return false; // Xung đột thời gian
            }

            existing.CustomerId = booking.CustomerId;
            existing.RoomId = booking.RoomId;
            existing.CheckInDate = booking.CheckInDate;
            existing.CheckOutDate = booking.CheckOutDate;
            existing.TotalPrice = booking.TotalPrice;
            existing.BookingStatus = booking.BookingStatus;
            existing.Customer = booking.Customer;
            existing.Room = booking.Room;
            return true;
        }

        public bool Delete(int id)
        {
            var booking = GetById(id);
            if (booking == null || booking.BookingStatus != 1)
            {
                return false;
            }

            booking.BookingStatus = 2; // Mark as Cancelled
            return true;
        }

        public List<Booking> Search(String customerId, DateTime? fromDate, DateTime? toDate)
        {
            var query = GetAll().AsQueryable();

            if (customerId !=null)
            {
                query = query.Where(b => b.Customer.CustomerFullName.Contains(customerId));
            }

            if (fromDate.HasValue)
            {
                query = query.Where(b => b.CheckInDate >= fromDate.Value);
            }

            if (toDate.HasValue)
            {
                query = query.Where(b => b.CheckOutDate <= toDate.Value);
            }

            return query.Where(b => b.BookingStatus == 1).ToList();
        }
        public decimal CalculateTotalPrice(int roomId, DateTime checkIn, DateTime checkOut)
        {
            if (checkIn >= checkOut)
                return 0;

            var room = _db.Rooms.FirstOrDefault(r => r.RoomID == roomId && r.RoomStatus == 1);
            if (room == null)
                return 0;

            var totalDays = (checkOut - checkIn).Days;
            return room.RoomPricePerDate * totalDays;
        }

    }
}