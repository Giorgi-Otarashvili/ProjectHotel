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
        Task<string> RegisterAsync(RegisterDTO registerDto);
        Task<string?> LoginAsync(LoginDTO loginDto);
    }
}
