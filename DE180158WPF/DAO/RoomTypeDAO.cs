using System.Collections.Generic;
using System.Linq;
using DE180158WPF.Models;

namespace DE180158WPF.DAO
{
    public class RoomTypeDAO
    {
        private readonly InMemoryDatabase _db = InMemoryDatabase.Instance;

        public List<RoomType> GetAll() => _db.RoomTypes;

        public RoomType? GetById(int id) =>
            _db.RoomTypes.FirstOrDefault(rt => rt.RoomTypeID == id);

        public void Add(RoomType roomType)
        {
            roomType.RoomTypeID = _db.NextRoomTypeId();
            _db.RoomTypes.Add(roomType);
        }

        public void Update(RoomType roomType)
        {
            var existing = GetById(roomType.RoomTypeID);
            if (existing != null)
            {
                existing.RoomTypeName = roomType.RoomTypeName;
                existing.TypeDescription = roomType.TypeDescription;
                existing.TypenNote = roomType.TypenNote;
            }
        }

        public void Delete(int id)
        {
            var roomType = GetById(id);
            if (roomType != null)
            {
                _db.RoomTypes.Remove(roomType);
            }
        }

        public List<RoomType> Search(string keyword) =>
            _db.RoomTypes
               .Where(rt => rt.RoomTypeName.Contains(keyword, System.StringComparison.OrdinalIgnoreCase) ||
                            rt.TypeDescription.Contains(keyword, System.StringComparison.OrdinalIgnoreCase))
               .ToList();
    }
}
