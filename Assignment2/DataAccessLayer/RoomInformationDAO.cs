using BusinessObject;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class RoomInformationDAO
    {
        private readonly FuminiHotelManagementContext _context;

        public RoomInformationDAO()
        {
            _context = new FuminiHotelManagementContext();
        }
        public List<RoomInformation> GetAvailableRooms()
        {
            return _context.RoomInformations
                .Include(r => r.RoomType)
                .Include(r => r.BookingDetails)
                .Where(r => r.RoomStatus == 1)
                .ToList();
        }

        public List<RoomInformation> GetAvailableRooms(DateOnly start, DateOnly end)
        {
            return _context.RoomInformations
                .Include(r => r.RoomType)
                .Include(r => r.BookingDetails)
                .Where(r => !r.BookingDetails.Any(d =>
                !(end <= d.StartDate || start >= d.EndDate))) // No overlap
                .ToList();
        }

        public List<RoomInformation> GetAll()
        {
            return _context.RoomInformations
                .Include(r => r.RoomType)
                //.Where(r => r.RoomStatus == 1)
                .ToList();
        }

        public RoomInformation? GetById(int id)
        {
            return _context.RoomInformations
                .Include(r => r.RoomType)
                .FirstOrDefault(r => r.RoomId == id);
        }

        public bool Add(RoomInformation room)
        {
            if (_context.RoomInformations.Any(r =>
                r.RoomNumber.ToLower() == room.RoomNumber.ToLower()))
            {
                return false; // Room number already exists
            }

            _context.RoomInformations.Add(room);
            _context.SaveChanges();
            return true;
        }

        public bool Update(RoomInformation room)
        {
            var existing = _context.RoomInformations.FirstOrDefault(r => r.RoomId == room.RoomId);
            if (existing == null) return false;

            existing.RoomNumber = room.RoomNumber;
            existing.RoomDetailDescription = room.RoomDetailDescription;
            existing.RoomMaxCapacity = room.RoomMaxCapacity;
            existing.RoomStatus = room.RoomStatus;
            existing.RoomPricePerDay = room.RoomPricePerDay;
            existing.RoomTypeId = room.RoomTypeId;

            _context.SaveChanges();
            return true;
        }

        public void Delete(int id, bool deleteFor)
        {
            var room = _context.RoomInformations.FirstOrDefault(r => r.RoomId == id);
            if (room != null)
            {
                if (deleteFor)
                {
                    _context.RoomInformations.Remove(room);
                }
                else
                {
                    room.RoomStatus = 2;
                }
                _context.SaveChanges();
            }
        }

        public List<RoomInformation> Search(string keyword)
        {
            return _context.RoomInformations
                .Include(r => r.RoomType)
                .Where(r => r.RoomStatus == 1 &&
                            (EF.Functions.Like(r.RoomNumber, $"%{keyword}%") ||
                             EF.Functions.Like(r.RoomDetailDescription!, $"%{keyword}%")))
                .ToList();
        }
    }
}

