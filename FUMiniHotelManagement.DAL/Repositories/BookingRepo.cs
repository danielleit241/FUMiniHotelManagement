using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FUMiniHotelManagement.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace FUMiniHotelManagement.DAL.Repositories
{
    public class BookingRepo
    {
        private FuminiHotelManagementContext _context;

        public List<BookingReservation> GetAll()
        {
            _context = new();
            return _context.BookingReservations.Include("BookingDetails").Include("Customer").ToList();
        }

        public BookingReservation? GetOne(int id)
        {
            _context = new();
            return _context.BookingReservations.FirstOrDefault(b => b.BookingReservationId == id);
        }

        public List<BookingReservation>? GetAllByCustomerId(int id)
        {
            _context = new();
            return _context.BookingReservations.Where(b => b.CustomerId == id).Include("Customer").Include(b => b.BookingDetails).ToList();
        }

        public void Create(BookingReservation x)
        {
            _context = new();
            _context.BookingReservations.Add(x);
            _context.SaveChanges();
        }

        public void Update(BookingReservation x)
        {
            _context = new();
            _context.BookingReservations.Update(x);
            _context.SaveChanges();
        }

        public void Delete(BookingReservation x)
        {
            _context = new();
            _context.BookingReservations.Update(x);
            _context.SaveChanges();
        }

        public decimal GetTotalPrice()
        {
            _context = new();
            return _context.BookingReservations.Sum(b => b.TotalPrice ?? 0);
        }

        public int GetTotalRoomsBooked()
        {
            _context = new();
            return _context.BookingDetails.Count();
        }
    }
}
