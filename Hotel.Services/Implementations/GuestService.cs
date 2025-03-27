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
    public class GuestService : IGuestService
    {
        private readonly IGuestRepository _guestRepository;

        public GuestService(IGuestRepository guestRepository)
        {
            _guestRepository = guestRepository;
        }

        public async Task<string> RegisterGuestAsync(GuestDTO guestDTO)
        {
            var existingGuest = await _guestRepository.GetByPersonalIdAsync(guestDTO.PersonalId);
            if (existingGuest != null)
                return "Guest with this personal ID already exists.";

            var guest = new Guest
            {
                FirstName = guestDTO.FirstName,
                LastName = guestDTO.LastName,
                PersonalId = guestDTO.PersonalId,
                PhoneNumber = guestDTO.PhoneNumber
            };

            await _guestRepository.AddAsync(guest);

            return "Guest registered successfully!";
        }

        public async Task<string> UpdateGuestAsync(int id, GuestDTO guestDTO)
        {
            var guest = await _guestRepository.GetByIdAsync(id);
            if (guest == null)
                return "Guest not found.";

            guest.FirstName = guestDTO.FirstName;
            guest.LastName = guestDTO.LastName;
            guest.PhoneNumber = guestDTO.PhoneNumber;

            await _guestRepository.UpdateAsync(guest);

            return "Guest details updated successfully!";
        }

        public async Task<string> DeleteGuestAsync(int id)
        {
            var guest = await _guestRepository.GetByIdAsync(id);
            if (guest == null)
                return "Guest not found.";

            // Check if the guest has active reservations
            if (await _guestRepository.HasActiveReservationsAsync(id))
                return "Guest has active reservations and cannot be deleted.";

            await _guestRepository.DeleteAsync(guest);
            return "Guest deleted successfully!";
        }

    }
}
