using Hotel.Models.Entities;
using Hotel.Repository.Data;
using Hotel.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Repository.Implementations
{
    public class HotelRepository : RepositoryBase<hotel>, IHotelRepository
    {
        public HotelRepository(ApplicationDbContext context) : base(context)
        {
        }

        public IQueryable<hotel> GetHotelsByFilter(string? country, string? city, int? rating)
        {
            var query = _dbSet.AsQueryable();

            query = query.Include(c => c.Rooms);

            if (!string.IsNullOrEmpty(country))
                query = query.Where(h => h.Country == country);

            if (!string.IsNullOrEmpty(city))
                query = query.Where(h => h.City == city);

            if (rating.HasValue)
                query = query.Where(h => h.Rating == rating.Value);

            return query;
        }

        public async Task<bool> CanDeleteHotelAsync(int hotelId)
        {
            var hasActiveRooms = await _context.Rooms.AnyAsync(r => r.HotelId == hotelId && r.IsAvailable);
            var hasActiveReservations = await _context.Reservations
                .AnyAsync(r => _context.Rooms.Any(room => room.Id == r.RoomId && room.HotelId == hotelId)
                               //&& r.CheckOut > DateTime.UtcNow
                               );

            return !hasActiveRooms && !hasActiveReservations;
        }
    }
}
