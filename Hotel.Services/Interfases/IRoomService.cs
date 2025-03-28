using Hotel.Models.Dtos;
using Hotel.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Services.Interfases
{
    public interface IRoomService
    {
        Task<Room> AddRoomAsync(RoomCreateDto roomDTO, int hotelId);
        Task<Room> UpdateRoomAvailabilityAsync(int roomId, bool isAvailable);
        Task<Room> UpdateRoomAsync(int roomId, string name, decimal price, bool isAvailable);
        Task<bool> DeleteRoomAsync(int roomId);
        IQueryable<Room> GetRoomsByFilter(int? hotelId, bool? isAvailable, decimal? minPrice, decimal? maxPrice);
        Task<Room> GetRoomByIdAsync(int roomId);
    }
}
