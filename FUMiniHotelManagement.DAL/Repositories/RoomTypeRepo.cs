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
