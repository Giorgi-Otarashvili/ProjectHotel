using Hotel.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Repository.Interfaces
{
    public interface IGuestRepository : IRepositoryBase<Guest>
    {
        Task<Guest> GetByPersonalIdAsync(string personalId);
        Task<bool> HasActiveReservationsAsync(int guestId);
    }
}
