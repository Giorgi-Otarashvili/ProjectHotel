using Hotel.Models.Dtos;
using Hotel.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Services.Interfases
{
    public interface IReservationService
    {
        Task<Reservation?> CreateReservationAsync(ReservationCreateDto reservationDTO);
        Task<bool> DeleteReservationAsync(int reservationId);
        Task<bool> UpdateReservationAsync(int reservationId, DateTime checkIn, DateTime checkOut);
    }
}
