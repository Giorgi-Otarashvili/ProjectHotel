using AutoMapper;
using Hotel.Models.Dtos;
using Hotel.Models.Entities;
using Hotel.Repository.Interfaces;
using Hotel.Services.Exceptions;
using Hotel.Services.Interfases;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Services.Implementations
{
    public class HotelService : IHotelService
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;


        public HotelService(IHotelRepository hotelRepository, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _hotelRepository = hotelRepository;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<hotel> CreateHotelAsync(HotelCreateDto hotelDto)
        {
            var rooms = _mapper.Map<List<Room>>(hotelDto.Rooms);

            var hotel = _mapper.Map<hotel>(hotelDto);

            hotel.Rooms = rooms;

            if (hotel.Rating < 1 || hotel.Rating > 5)
                throw new RatingValidationException("Rating must be between 1 and 5.");

            await _hotelRepository.AddAsync(hotel);

            var manager = await _userManager.FindByIdAsync(hotelDto.ManagerId);
            manager.HotelId = hotel.Id;
            await _userManager.UpdateAsync(manager);

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

        public async Task<bool> AddManagerToHotelAsync(string managerId, int hotelId)
        {
            try
            {
                var hotel = await _hotelRepository.GetByIdAsync(hotelId);

                var manager = await _userManager.FindByIdAsync(managerId);

                manager.HotelId = hotelId;
                hotel.ManagerId = managerId;

                await _userManager.UpdateAsync(manager);

                await _hotelRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) 
            {
                return false;
            }
        }
    }
}
