using Microsoft.AspNetCore.Mvc;
using Hotel.Models.Dtos;
using Hotel.Services.Implementations;
using Hotel.Services.Interfases;

namespace Hotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register-guest")]
        public async Task<IActionResult> RegisterGuest([FromBody] RegisterDTO registerDto)
        {
            var result = await _authService.RegisterGuestAsync(registerDto);
            if (result == "User registered successfully!")
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("register-administrator")]
        public async Task<IActionResult> RegisterAdministrator([FromBody] RegisterDTO registerDto)
        {
            var result = await _authService.RegisterAdminAsync(registerDto);
            if (result == "User registered successfully!")
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("register-manager")]
        public async Task<IActionResult> RegisterManager([FromBody] RegisterDTO registerDto)
        {
            var result = await _authService.RegisterManagerAsync(registerDto);
            if (result == "User registered successfully!")
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            var token = await _authService.LoginAsync(loginDto);
            if (token == null)
                return Unauthorized("Invalid credentials");

            return Ok(new { Token = token });
        }
    }
}
