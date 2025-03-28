﻿using AutoMapper;
using Hotel.Models.Dtos;
using Hotel.Models.Entities;
using Hotel.Repository.Interfaces;
using Hotel.Services.Interfases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Services.Implementations
{
    public class RoomService : IRoomService
    {
        private readonly IRoomsRepository _roomsRepository;
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;

        public RoomService(IRoomsRepository roomsRepository, IHotelRepository hotelRepository, IMapper mapper)
        {
            _roomsRepository = roomsRepository;
            _hotelRepository = hotelRepository;
            _mapper = mapper;
        }
        public async Task<Room> AddRoomAsync(RoomCreateDto roomDTO, int hotelId)
        {
            var hotel = await _hotelRepository.GetByIdAsync(hotelId);
            if (hotel == null)
                throw new Exception("Hotel not found.");

            if (roomDTO.Price <= 0)
                throw new Exception("Room price must be greater than zero.");

            var room = _mapper.Map<Room>(roomDTO);
            room.HotelId = hotelId;

            await _roomsRepository.AddAsync(room);
            return room;
        }

        public async Task<Room> UpdateRoomAvailabilityAsync(int roomId, bool isAvailable)
        {
            var room = await _roomsRepository.GetByIdAsync(roomId);
            if (room == null)
                throw new Exception("Room not found.");

            room.IsAvailable = isAvailable;
            await _roomsRepository.UpdateAsync(room);

            return room;
        }
        public async Task<Room> UpdateRoomAsync(int roomId, string name, decimal price, bool isAvailable)
        {
            var room = await _roomsRepository.GetByIdAsync(roomId);
            if (room == null)
                throw new Exception("Room not found.");

            if (price <= 0)
                throw new Exception("Price must be greater than zero.");

            room.Name = name;
            room.Price = price;
            room.IsAvailable = isAvailable;

            await _roomsRepository.UpdateAsync(room);
            return room;
        }

        // 4️⃣ ოთახის წაშლა (თუ არ აქვს აქტიური ჯავშნები)
        public async Task<bool> DeleteRoomAsync(int roomId)
        {
            var room = await _roomsRepository.GetByIdAsync(roomId);
            if (room == null)
                throw new Exception("Room not found.");

            // აქ მოგვიანებით უნდა დავამატოთ ActiveBookings ლოგიკა
            await _roomsRepository.DeleteAsync(room);
            return true;
        }
        public IQueryable<Room> GetRoomsByFilter(int? hotelId, bool? isAvailable, decimal? minPrice, decimal? maxPrice)
        {
            var rooms = _roomsRepository.GetAll();

            if (hotelId.HasValue)
                rooms = rooms.Where(r => r.HotelId == hotelId.Value);

            if (isAvailable.HasValue)
                rooms = rooms.Where(r => r.IsAvailable == isAvailable.Value);

            if (minPrice.HasValue)
                rooms = rooms.Where(r => r.Price >= minPrice.Value);

            if (maxPrice.HasValue)
                rooms = rooms.Where(r => r.Price <= maxPrice.Value);

            return rooms; // IQueryable-ით ვაბრუნებთ, რაც უკეთესია SQL-თვის
        }

        public Task<Room> GetRoomByIdAsync(int roomId)
        {
            return _roomsRepository.GetByIdAsync(roomId);
        }
    }
}
