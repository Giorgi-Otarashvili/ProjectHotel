using Hotel.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Services.Interfases
{
    public interface IAuthService
    {
        Task<string> RegisterGuestAsync(RegisterDTO registerDto);
        Task<string> RegisterAdminAsync(RegisterDTO registerDto);
        Task<string> RegisterManagerAsync(RegisterDTO registerDto);
        Task<string?> LoginAsync(LoginDTO loginDto);
    }
}
