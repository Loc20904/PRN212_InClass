using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DE180158WPF.Models
{
    public class RoomInformation
    {
        public int RoomID { get; set; }
        public string RoomNumber { get; set; } = string.Empty;
        public string RoomDescription { get; set; } = string.Empty;
        public int RoomMaxCapacity { get; set; }
        public int RoomStatus { get; set; }
        public decimal RoomPricePerDate { get; set; }

        // Foreign Key
        public int RoomTypeID { get; set; }

        // Navigation Property
        public RoomType? RoomType { get; set; }
    }
}
