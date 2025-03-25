using Hotel.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Repository.Interfaces
{
    public interface IHotelRepository : IRepositoryBase<hotel>
    {
        IQueryable<hotel> GetHotelsByFilter(string? country, string? city, int? rating);
        Task<bool> CanDeleteHotelAsync(int hotelId);
    }
}
