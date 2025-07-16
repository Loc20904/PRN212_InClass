using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;
using Repositories;

namespace Services
{
    public class BookingReservationService
    {
        private readonly BookingReservationRepository _repository;

        public BookingReservationService()
        {
            _repository = new BookingReservationRepository();
        }

        public List<BookingReservation> GetAll() => _repository.GetAll();
        public BookingReservation? GetById(int id) => _repository.GetById(id);
        public void Add(BookingReservation booking) => _repository.Add(booking);
        public void Update(BookingReservation booking) => _repository.Update(booking);
        public void Delete(int id) => _repository.Delete(id);
    }
}
