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

        public List<BookingReservation> GetOne()
        {
            _context = new();
            return _context.BookingReservations.Include("BookingDetails").Include("Customer").ToList();
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
    }
}
