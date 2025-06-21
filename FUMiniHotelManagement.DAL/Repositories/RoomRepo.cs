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
            return _context.RoomInformations.Include("RoomType").ToList();
        }
        public RoomInformation? GetOne(int id)
        {
            _context = new();
            return _context.RoomInformations.FirstOrDefault(r => r.RoomId == id);
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

        public List<RoomInformation> SearchByDesOrPrice(string des, decimal? price)
        {
            _context = new();
            if (string.IsNullOrEmpty(des) && !price.HasValue)
            {
                return GetAll();
            }

            if (!string.IsNullOrEmpty(des) && price.HasValue)
            {
                return _context.RoomInformations.Where(r => r.RoomDetailDescription!.Contains(des) || r.RoomPricePerDay == price)
                                                 .Include("RoomType").ToList();
            }
            else if (!string.IsNullOrEmpty(des))
            {
                return _context.RoomInformations.Where(r => r.RoomDetailDescription!.Contains(des))
                                                 .Include("RoomType").ToList();
            }
            else
            {
                return _context.RoomInformations.Where(r => r.RoomPricePerDay == price)
                                                 .Include("RoomType").ToList();
            }
        }
    }
}
