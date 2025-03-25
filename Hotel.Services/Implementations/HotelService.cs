using Hotel.Models.Entities;
using Hotel.Repository.Interfaces;
using Hotel.Services.Exceptions;
using Hotel.Services.Interfases;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Services.Implementations
{
    public class HotelService : IHotelService
    {
        private readonly IHotelRepository _hotelRepository;

        public HotelService(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        public async Task<hotel> CreateHotelAsync(hotel hotel)
        {
            if (hotel.Rating < 1 || hotel.Rating > 5)
                throw new RatingValidationException("Rating must be between 1 and 5.");

            await _hotelRepository.AddAsync(hotel);


            return hotel;
        }


        public async Task<hotel> UpdateHotelAsync(int hotelId, string name, string address, int rating)
        {
            var hotel = await _hotelRepository.GetByIdAsync(hotelId);
            if (hotel == null)
                throw new KeyNotFoundException("Hotel not found.");

            if (rating < 1 || rating > 5)
                throw new ValidationException("Rating must be between 1 and 5.");

            hotel.Name = name;
            hotel.Address = address;
            hotel.Rating = rating;

            await _hotelRepository.UpdateAsync(hotel);
            return hotel;
        }

        public async Task<bool> DeleteHotelAsync(int hotelId)
        {
            var hotel = await _hotelRepository.GetByIdAsync(hotelId);
            if (hotel == null) return false; 

            await _hotelRepository.DeleteAsync(hotel); 
            return true;
        }

        public IQueryable<hotel> GetHotelsByFilter(string? country, string? city, int? rating)
        {
            return _hotelRepository.GetHotelsByFilter(country, city, rating);
        }

        
        public async Task<hotel?> GetHotelByIdAsync(int hotelId)
        {
            return await _hotelRepository.GetByIdAsync(hotelId);
        }
    }
}
