using BusinessObject;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class RoomTypeDAO
    {
        private readonly FuminiHotelManagementContext _context;

        public RoomTypeDAO()
        {
            _context = new FuminiHotelManagementContext();
        }

        public List<RoomType> GetAll()
        {
            return _context.RoomTypes
                .Include(rt => rt.RoomInformations)
                .ToList();
        }

        public RoomType? GetById(int id)
        {
            return _context.RoomTypes
                .Include(rt => rt.RoomInformations)
                .FirstOrDefault(rt => rt.RoomTypeId == id);
        }

        public void Add(RoomType roomType)
        {
            _context.RoomTypes.Add(roomType);
            _context.SaveChanges();
        }

        public void Update(RoomType roomType)
        {
            var existing = _context.RoomTypes.FirstOrDefault(rt => rt.RoomTypeId == roomType.RoomTypeId);
            if (existing != null)
            {
                existing.RoomTypeName = roomType.RoomTypeName;
                existing.TypeDescription = roomType.TypeDescription;
                existing.TypeNote = roomType.TypeNote;

                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var roomType = _context.RoomTypes.FirstOrDefault(rt => rt.RoomTypeId == id);
            if (roomType != null)
            {
                _context.RoomTypes.Remove(roomType);
                _context.SaveChanges();
            }
        }

        public List<RoomType> Search(string keyword)
        {
            return _context.RoomTypes
                .Where(rt =>
                    EF.Functions.Like(rt.RoomTypeName, $"%{keyword}%") ||
                    EF.Functions.Like(rt.TypeDescription ?? "", $"%{keyword}%"))
                .ToList();
        }
    }
}
