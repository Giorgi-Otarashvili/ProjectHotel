using Hotel.Models.Entities;
using Hotel.Repository.Interfaces;
using Hotel.Services.Interfases;
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

        // ახალი სასტუმროს შექმნა
        public async Task<hotel> CreateHotelAsync(hotel hotel)
        {
            await _hotelRepository.AddAsync(hotel); // დამატება
            return hotel;
        }

        // არსებული სასტუმროს განახლება
        public async Task<hotel> UpdateHotelAsync(int hotelId, string name, string address, int rating)
        {
            var hotel = await _hotelRepository.GetByIdAsync(hotelId); // მოძებნა
            if (hotel == null) return null; // თუ სასტუმრო ვერ მოიძებნა

            hotel.Name = name;
            hotel.Address = address;
            hotel.Rating = rating;

            await _hotelRepository.UpdateAsync(hotel); // განახლება
            return hotel;
        }

        // სასტუმროს წაშლა
        public async Task<bool> DeleteHotelAsync(int hotelId)
        {
            var hotel = await _hotelRepository.GetByIdAsync(hotelId); // მოძებნა
            if (hotel == null) return false; // თუ სასტუმრო ვერ მოიძებნა

            await _hotelRepository.DeleteAsync(hotel); // წაშლა
            return true;
        }

        // ფილტრი
        public IQueryable<hotel> GetHotelsByFilter(string? country, string? city, int? rating)
        {
            return _hotelRepository.GetHotelsByFilter(country, city, rating);
        }

        // ID-ს მიხედვით სასტუმროს მოძებნა
        public async Task<hotel?> GetHotelByIdAsync(int hotelId)
        {
            return await _hotelRepository.GetByIdAsync(hotelId);
        }
    }
}
