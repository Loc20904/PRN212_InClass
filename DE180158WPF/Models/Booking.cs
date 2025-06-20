using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DE180158WPF.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int RoomId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalPrice { get; set; }
        public int BookingStatus { get; set; } // 1: Active, 2: Cancelled, 3: Completed

        public Customer? Customer { get; set; }
        public RoomInformation? Room { get; set; }
    }
}
