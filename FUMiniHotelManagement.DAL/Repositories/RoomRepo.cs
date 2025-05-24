using FUMiniHotelManagement.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace FUMiniHotelManagement.DAL.Repositories
{
    public class RoomRepo
    {
        private FuminiHotelManagementContext _context;
        public List<RoomInformation> GetAll()
        {
            _context = new();
            return _context.RoomInformations.Include("RoomType").Include("BookingDetails").ToList();
        }
        public List<RoomInformation> GetOne()
        {
            _context = new();
            return _context.RoomInformations.Include("RoomType").Include("BookingDetails").ToList();
        }
        public void Create(RoomInformation x)
        {
            _context = new();
            _context.RoomInformations.Add(x);
            _context.SaveChanges();
        }
        public void Update(RoomInformation x)
        {
            _context = new();
            _context.RoomInformations.Update(x);
            _context.SaveChanges();
        }
        public void Delete(RoomInformation x)
        {
            _context = new();
            _context.RoomInformations.Update(x);
            _context.SaveChanges();
        }
    }
}
