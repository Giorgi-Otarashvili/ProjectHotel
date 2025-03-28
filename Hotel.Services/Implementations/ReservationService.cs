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
    public class ReservationService : IReservationService
    {
        private readonly IReservationsRepository _reservationsRepository;
        private readonly IRoomsRepository _roomsRepository;

        public ReservationService(IReservationsRepository reservationsRepository, IRoomsRepository roomsRepository)
        {
            _reservationsRepository = reservationsRepository;
            _roomsRepository = roomsRepository;
        }

        public async Task<Reservation?> CreateReservationAsync(ReservationCreateDto reservationDTO)
        {
            var room = await _roomsRepository.GetByIdAsync(reservationDTO.RoomId);

            var reservation = new Reservation()
            {
                CheckIn = reservationDTO.CheckIn,
                CheckOut = reservationDTO.CheckOut,
                RoomId = reservationDTO.RoomId,
                GuestId = reservationDTO.GuestId
            };

            await _reservationsRepository.AddAsync(reservation);
            await _reservationsRepository.SaveChangesAsync();

            room.IsAvailable = false;
            await _roomsRepository.UpdateAsync(room);
            await _roomsRepository.SaveChangesAsync();

            return reservation;

        }

        public async Task<bool> DeleteReservationAsync(int reservationId)
        {
            var reservation = await _reservationsRepository.GetByIdAsync(reservationId);
            if (reservation == null)
                return false;

            var room = await _roomsRepository.GetByIdAsync(reservation.RoomId);
            room.IsAvailable = true;
            await _roomsRepository.UpdateAsync(room);
            await _roomsRepository.SaveChangesAsync();
            await _reservationsRepository.DeleteAsync(reservation);
            await _reservationsRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateReservationAsync(int reservationId, DateTime checkIn, DateTime checkOut)
        {
            try
            {
                var reservation = await _reservationsRepository.GetByIdAsync(reservationId);

                var reservations = _reservationsRepository.GetAll().Where(c => c.RoomId == reservation.RoomId);

                foreach (var res in reservations)
                {
                    if (res.CheckIn < checkOut && res.CheckOut > checkIn)
                        return false;
                }

                reservation.CheckIn = checkIn;
                reservation.CheckOut = checkOut;
                await _reservationsRepository.UpdateAsync(reservation);
                await _reservationsRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
