using Hotel.Models.Entities;
using Hotel.Repository.Data;
using Hotel.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Repository.Implementations
{
    public class GuestRepository : RepositoryBase<Guest>, IGuestRepository
    {
        public GuestRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Guest> GetByPersonalIdAsync(string personalId)
        {
            return await _dbSet.FirstOrDefaultAsync(g => g.PersonalId == personalId);
        }

        public async Task<bool> HasActiveReservationsAsync(int guestId)
        {
            return await _context.Reservations.AnyAsync(r => r.GuestId == guestId && r.CheckOut > DateTime.UtcNow);
        }
    }
}
