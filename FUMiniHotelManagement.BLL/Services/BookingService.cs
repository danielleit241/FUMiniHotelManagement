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

        public decimal GetTotalPrice()
        {
            return _repo.GetTotalPrice();
        }

        public int GetTotalBookingCount()
        {
            return _repo.GetTotalRoomsBooked();
        }

    }
}
