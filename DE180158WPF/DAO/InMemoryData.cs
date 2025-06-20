using System;
using System.Collections.Generic;
using DE180158WPF.Models;

namespace DE180158WPF.DAO
{
    public class InMemoryDatabase
    {
        // ===== Singleton =====
        private static readonly Lazy<InMemoryDatabase> _instance =
            new(() => new InMemoryDatabase());

        public static InMemoryDatabase Instance => _instance.Value;

        // ===== Bộ sưu tập dữ liệu =====
        public List<Customer> Customers { get; private set; }
        public List<RoomType> RoomTypes { get; private set; }
        public List<RoomInformation> Rooms { get; private set; }
        public List<Booking> Bookings { get; private set; }

        // ===== Hàm tạo riêng (private) =====
        private InMemoryDatabase()
        {
            SeedData();
            // Sau khi SeedData() gọi xong
            var today = DateTime.Today;

            // ➊ Lấy booking gần nhất cho mỗi phòng
            var latestBookingsByRoom = Bookings
                .GroupBy(b => b.RoomId)
                .Select(g => g.OrderByDescending(b => b.CheckOutDate).First());

            foreach (var latest in latestBookingsByRoom)
            {
                // ➋ Cập nhật trạng thái booking nếu đã qua ngày trả
                if (latest.BookingStatus == 1 && latest.CheckOutDate < today)
                {
                    latest.BookingStatus = 3; // Completed
                }

                // ➌ Lấy phòng tương ứng
                var room = Rooms.FirstOrDefault(r => r.RoomID == latest.RoomId);
                if (room == null) continue;

                // ➍ Nếu booking gần nhất đã hoàn thành / hủy HOẶC đã qua ngày trả
                bool bookingFinished = latest.BookingStatus is 2 or 3 || latest.CheckOutDate < today;

                if (bookingFinished)
                {
                    room.RoomStatus = 1; // Phòng mở lại
                }
                else
                {
                    // Bạn có thể đặt RoomStatus = 2 (đang được đặt) tuỳ convention
                    room.RoomStatus = 2;
                }
            }

        }

        // ===== Khởi tạo dữ liệu mẫu =====
        private void SeedData()
        {
            // 1. RoomType
            RoomTypes = new List<RoomType>
            {
                new() { RoomTypeID = 1, RoomTypeName = "Standard", TypeDescription = "Phòng tiêu chuẩn", TypenNote = "" },
                new() { RoomTypeID = 2, RoomTypeName = "Deluxe", TypeDescription = "Phòng cao cấp", TypenNote = "" },
                new() { RoomTypeID = 3, RoomTypeName = "Suite", TypeDescription = "Phòng hạng sang", TypenNote = "" }
            };

            // 2. RoomInformation (5 bản ghi)
            Rooms = new List<RoomInformation>
            {
                new()
                {
                    RoomID = 1,
                    RoomNumber = "101",
                    RoomDescription = "Hướng vườn, 1 giường đôi",
                    RoomMaxCapacity = 2,
                    RoomStatus = 1,
                    RoomPricePerDate = 800_000m,
                    RoomTypeID = 1
                },
                new()
                {
                    RoomID = 2,
                    RoomNumber = "201",
                    RoomDescription = "Hướng biển, 1 giường King",
                    RoomMaxCapacity = 2,
                    RoomStatus = 1,
                    RoomPricePerDate = 1_500_000m,
                    RoomTypeID = 2
                },
                new()
                {
                    RoomID = 3,
                    RoomNumber = "301",
                    RoomDescription = "Phòng suite, ban công lớn",
                    RoomMaxCapacity = 4,
                    RoomStatus = 1,
                    RoomPricePerDate = 2_500_000m,
                    RoomTypeID = 3
                },
                new()
                {
                    RoomID = 4,
                    RoomNumber = "102",
                    RoomDescription = "Phòng thường, gần thang máy",
                    RoomMaxCapacity = 2,
                    RoomStatus = 2, // deleted
                    RoomPricePerDate = 750_000m,
                    RoomTypeID = 1
                },
                new()
                {
                    RoomID = 5,
                    RoomNumber = "202",
                    RoomDescription = "Hướng hồ, 2 giường đơn",
                    RoomMaxCapacity = 2,
                    RoomStatus = 1,
                    RoomPricePerDate = 1_200_000m,
                    RoomTypeID = 2
                }
            };

            // 3. Customer
            Customers = new List<Customer>
            {
                new()
                {
                    CustomerID = 2,
                    CustomerFullName = "Nguyễn Văn A",
                    Telephone = "0912345678",
                    EmailAddress = "a",
                    CustomerBirthday = new DateTime(2001, 5, 10),
                    CustomerStatus = 1,
                    Password = "123"
                },
                new()
                {
                    CustomerID = 3,
                    CustomerFullName = "Trần Thị B",
                    Telephone = "0987654321",
                    EmailAddress = "ttb@gmail.com",
                    CustomerBirthday = new DateTime(1998, 12, 2),
                    CustomerStatus = 1,
                    Password = "secret456"
                },
                new()
                {
                    CustomerID = 4,
                    CustomerFullName = "Lê Minh C",
                    Telephone = "0909001122",
                    EmailAddress = "lmc@gmail.com",
                    CustomerBirthday = new DateTime(1995, 3, 15),
                    CustomerStatus = 2, // deleted
                    Password = "qwerty789"
                }
            };

            // 4. Bookings (5 bản ghi)
            Bookings = new List<Booking>
            {
                new()
                {
                    Id = 1,
                    CustomerId = 2,
                    RoomId = 1,
                    CheckInDate = new DateTime(2025, 6, 20),
                    CheckOutDate = new DateTime(2025, 6, 22),
                    TotalPrice = 1_600_000m, // 2 ngày * 800,000
                    BookingStatus = 1 // Active
                },
                new()
                {
                    Id = 2,
                    CustomerId = 3,
                    RoomId = 2,
                    CheckInDate = new DateTime(2025, 6, 25),
                    CheckOutDate = new DateTime(2025, 6, 27),
                    TotalPrice = 3_000_000m, // 2 ngày * 1,500,000
                    BookingStatus = 1 // Active
                },
                new()
                {
                    Id = 3,
                    CustomerId = 2,
                    RoomId = 3,
                    CheckInDate = new DateTime(2025, 6, 15),
                    CheckOutDate = new DateTime(2025, 6, 18),
                    TotalPrice = 7_500_000m, // 3 ngày * 2,500,000
                    BookingStatus = 2 // Cancelled
                },
                new()
                {
                    Id = 4,
                    CustomerId = 3,
                    RoomId = 5,
                    CheckInDate = new DateTime(2025, 7, 1),
                    CheckOutDate = new DateTime(2025, 7, 3),
                    TotalPrice = 2_400_000m, // 2 ngày * 1,200,000
                    BookingStatus = 1 // Active
                },
                new()
                {
                    Id = 5,
                    CustomerId = 2,
                    RoomId = 2,
                    CheckInDate = new DateTime(2025, 7, 5),
                    CheckOutDate = new DateTime(2025, 7, 8),
                    TotalPrice = 4_500_000m, // 3 ngày * 1,500,000
                    BookingStatus = 2
                }
            };
        }

        // ===== Tiện ích tạo ID tự tăng =====
        public int NextCustomerId() => Customers.Count == 0 ? 1 : Customers[^1].CustomerID + 1;
        public int NextRoomId() => Rooms.Count == 0 ? 1 : Rooms[^1].RoomID + 1;
        public int NextRoomTypeId() => RoomTypes.Count == 0 ? 1 : RoomTypes[^1].RoomTypeID + 1;
        public int NextBookingId() => Bookings.Count == 0 ? 1 : Bookings[^1].Id + 1;
    }
}