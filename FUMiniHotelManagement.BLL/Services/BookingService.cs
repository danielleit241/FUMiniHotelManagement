using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FUMiniHotelManagement.DAL.Entities;
using FUMiniHotelManagement.DAL.Repositories;

namespace FUMiniHotelManagement.BLL.Services
{
    public class BookingService
    {
        private readonly BookingRepo _repo = new();
        public List<BookingReservation>? GetBookingReservationsByCustomerId(int customerId)
        {
            return _repo.GetAllByCustomerId(customerId);
        }

        public List<BookingReservation> GetAllBookingReservations()
        {
            return _repo.GetAll();
        }
    }
}
