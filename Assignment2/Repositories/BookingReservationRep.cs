using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;
using DataAccessLayer;

namespace Repositories
{
    public class BookingReservationRepository
    {
        private readonly BookingReservationDAO _dao;

        public BookingReservationRepository()
        {
            _dao = new BookingReservationDAO();
        }

        public List<BookingReservation> GetAll() => _dao.GetAll();
        public BookingReservation? GetById(int id) => _dao.GetById(id);
        public void Add(BookingReservation booking) => _dao.Add(booking);
        public void Update(BookingReservation booking) => _dao.Update(booking);
        public void Delete(int id) => _dao.Delete(id);
    }
}
