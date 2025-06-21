using FUMiniHotelManagement.DAL.Entities;
using FUMiniHotelManagement.DAL.Repositories;

namespace FUMiniHotelManagement.BLL.Services
{
    public class RoomTypeService
    {
        private RoomTypeRepo _repo = new();
        public List<RoomType> GetRoomTypes()
        {
            return _repo.GetAll();
        }
    }
}
