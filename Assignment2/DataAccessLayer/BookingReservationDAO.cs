using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using BusinessObject;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class BookingReservationDAO
    {
        private readonly FuminiHotelManagementContext _context;

        public BookingReservationDAO()
        {
            _context = new FuminiHotelManagementContext();
        }

        public List<BookingReservation> GetAll() =>
            _context.BookingReservations
                    .Include(b => b.Customer)
                    .Include(b => b.BookingDetails)
                        .ThenInclude(d => d.Room)
                    .ToList();

        public BookingReservation? GetById(int id) =>
            _context.BookingReservations
                    .Include(b => b.BookingDetails)
                    .FirstOrDefault(b => b.BookingReservationId == id);

        public void Add(BookingReservation booking)
        {
            _context.BookingReservations.Add(booking);
            _context.SaveChanges();
        }

        public void Update(BookingReservation booking)
        {

            var tracked = _context.BookingReservations
                .Local
                .FirstOrDefault(b => b.BookingReservationId == booking.BookingReservationId);

            if (tracked != null)
                _context.Entry(tracked).State = EntityState.Detached;

            _context.BookingReservations.Update(booking);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var b = _context.BookingReservations.Find(id);
            if (b != null)
            {
                _context.BookingReservations.Remove(b);
                _context.SaveChanges();
            }
        }
    }
}
