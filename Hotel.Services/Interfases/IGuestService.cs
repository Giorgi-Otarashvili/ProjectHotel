using Hotel.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Services.Interfases
{
    public interface IGuestService
    {
        Task<string> RegisterGuestAsync(GuestDTO guestDTO);
        Task<string> UpdateGuestAsync(int id, GuestDTO guestDTO);
        Task<string> DeleteGuestAsync(int id);
    }
}
