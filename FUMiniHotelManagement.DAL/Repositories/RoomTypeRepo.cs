using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FUMiniHotelManagement.DAL.Entities;

namespace FUMiniHotelManagement.DAL.Repositories
{
    public class RoomTypeRepo
    {
        private FuminiHotelManagementContext _context;

        public List<RoomType> GetAll()
        {
            _context = new();
            return _context.RoomTypes.ToList();
        }
    }
}
