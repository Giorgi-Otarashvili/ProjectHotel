using Hotel.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Repository.Interfaces
{
    public interface IRoomsRepository : IRepositoryBase<Room>
    {
        IQueryable<Room> GetAvailableRooms(int hotelId);
    }
}
