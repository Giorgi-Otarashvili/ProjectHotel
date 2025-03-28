using Microsoft.AspNetCore.Mvc;
using Hotel.Models.Dtos;
using Hotel.Services.Implementations;
using Hotel.Services.Interfases;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

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

        [HttpPut("update-profile")]
        [Authorize]
        public async Task<IActionResult> UpdateUserProfile([FromBody] UpdateUserDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized("User not found");

            var updated = await _authService.UpdateUserProfile(userId, model);
            if (!updated)
                return BadRequest("Failed to update profile");

            return Ok(new { message = "Profile updated successfully" });
        }

        [HttpDelete("delete-account")]
        public async Task<IActionResult> DeleteUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized("User not found");

            var deleted = await _authService.DeleteUser(userId);
            if (!deleted)
                return BadRequest("Failed to delete account");

            return Ok(new { message = "Account deleted successfully" });
        }
    }
}
