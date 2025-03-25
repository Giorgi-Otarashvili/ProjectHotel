using Hotel.Models.Entities;
using Hotel.Repository.Data;
using Hotel.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Repository.Implementations
{
    public class RoomsRepository : RepositoryBase<Room>, IRoomsRepository
    {
        public RoomsRepository(ApplicationDbContext context) : base(context)
        {
        }
        
        public IQueryable<Room> GetAvailableRooms()
        {
            return _dbSet.Where(r => r.IsAvailable);
        }

        public IQueryable<Room> GetAvailableRooms(int hotelId)
        {
            return _dbSet.Where(r => r.IsAvailable && r.HotelId == hotelId);
        }
    }
}
