using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FUMiniHotelManagement.DAL.Entities;
using FUMiniHotelManagement.DAL.Repositories;

namespace FUMiniHotelManagement.BLL.Services
{
    public class RoomService
    {
        private RoomRepo repo = new();

        public List<RoomInformation> GetRooms()
        {
            return repo.GetAll();
        }

        public RoomInformation? GetRoomById(int id)
        {
            return repo.GetOne(id);
        }

        public void CreateRoom(RoomInformation room)
        {
            repo.Create(room);
        }

        public void UpdateRoom(RoomInformation room)
        {
            repo.Update(room);
        }

        public void DeletableRoom(RoomInformation room)
        {
            repo.Delete(room);
        }

        public List<RoomInformation> SearchRoomsByDesOrPrice(string des, decimal? price)
        {
            return repo.SearchByDesOrPrice(des.ToLower().Trim(), price);
        }
    }
}
