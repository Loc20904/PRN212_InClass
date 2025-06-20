using System.Collections.Generic;
using System.Linq;
using DE180158WPF.Models;

namespace DE180158WPF.DAO
{
    public class RoomInformationDAO
    {
        private readonly InMemoryDatabase _db = InMemoryDatabase.Instance;

        public List<RoomInformation> GetAll()
        {
            return _db.Rooms
            .Where(r => r.RoomStatus == 1)
            .Select(r =>
            {
                r.RoomType = _db.RoomTypes.FirstOrDefault(rt => rt.RoomTypeID == r.RoomTypeID);
                return r;
            })
            .ToList();
        }
            

        public RoomInformation? GetById(int id) =>
            _db.Rooms.FirstOrDefault(r => r.RoomID == id);

        public bool Add(RoomInformation room)
        {
            if (_db.Rooms.Any(r => r.RoomNumber.Equals(room.RoomNumber, System.StringComparison.OrdinalIgnoreCase)))
            {
                return false; // Room number already exists
            }
            room.RoomID = _db.NextRoomId();
            _db.Rooms.Add(room);
            return true; // Add successful
        }

        public bool Update(RoomInformation room)
        {
            var existing = GetById(room.RoomID);
            if (existing != null)
            {
                existing.RoomNumber = room.RoomNumber;
                existing.RoomDescription = room.RoomDescription;
                existing.RoomMaxCapacity = room.RoomMaxCapacity;
                existing.RoomStatus = room.RoomStatus;
                existing.RoomPricePerDate = room.RoomPricePerDate;
                existing.RoomTypeID = room.RoomTypeID;
                return true; // Update successful
            }
            return false; // Update failed, room not found
        }

        public void Delete(int id)
        {
            var room = GetById(id);
            if (room != null)
            {
                room.RoomStatus = 2; // Mark as deleted
            }
        }

        public List<RoomInformation> Search(string keyword) =>
            _db.Rooms
               .Where(r => r.RoomStatus == 1 &&
                          (r.RoomNumber.Contains(keyword, System.StringComparison.OrdinalIgnoreCase) ||
                           r.RoomDescription.Contains(keyword, System.StringComparison.OrdinalIgnoreCase)))
               .ToList();

    }
}
