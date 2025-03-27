using Hotel.Models;
using Hotel.Models.Dtos;
using Hotel.Models.Entities;
using Hotel.Services.Interfases;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Hotel.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        private const string GuestRole = "Guest";
        private const string AdminRole = "Administrator";
        private const string ManagerRole = "Manager";

        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<string> RegisterGuestAsync(RegisterDTO registerDto)
        {
            await _roleManager.CreateAsync(new IdentityRole(GuestRole));
            await _roleManager.CreateAsync(new IdentityRole(AdminRole));
            await _roleManager.CreateAsync(new IdentityRole(ManagerRole));

            return await RegisterUserAsync(registerDto, GuestRole);
        }

        public async Task<string> RegisterAdminAsync(RegisterDTO registerDto)
        {
            return await RegisterUserAsync(registerDto, AdminRole);
        }

        public async Task<string> RegisterManagerAsync(RegisterDTO registerDto)
        {
            return await RegisterUserAsync(registerDto, ManagerRole);
        }

        public async Task<string?> LoginAsync(LoginDTO loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
                return null;

            var result = await _signInManager.PasswordSignInAsync(user, loginDto.Password, false, false);
            if (!result.Succeeded)
                return null;

            return GenerateToken(user);
        }

        private async Task<string> RegisterUserAsync(RegisterDTO registerDto, string role)
        {

            var roleEnum = role switch
            {
                GuestRole => RoleEnum.Guest,
                AdminRole => RoleEnum.Admin,
                ManagerRole => RoleEnum.Manager,
                _ => throw new DataException("Invalid role"),
            };


            var user = new ApplicationUser
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                UserName = registerDto.Email,
                PersonalId = registerDto.PersonalId,
                PhoneNumber = registerDto.PhoneNumber,
                Role = roleEnum
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
                return string.Join(", ", result.Errors.Select(e => e.Description));

            await _userManager.AddToRoleAsync(user, role);

            return "User registered successfully!";
        }

        private string GenerateToken(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}")
            };

            var roles = _userManager.GetRolesAsync(user).Result;
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
