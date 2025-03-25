using Hotel.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Services.Interfases
{
    public interface IHotelService
    {
        Task<hotel> CreateHotelAsync(hotel hotel);

        Task<hotel> UpdateHotelAsync(int hotelId, string name , string address, int rating);

        Task<bool> DeleteHotelAsync(int hotelId);

        IQueryable<hotel> GetHotelsByFilter(string? country, string? city, int? rating);
        //IQueryable<hotel> GetHotelById(int hotelId);
        Task<hotel?> GetHotelByIdAsync(int hotelId);


    }
}