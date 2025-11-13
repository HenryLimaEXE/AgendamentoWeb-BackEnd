using Microsoft.EntityFrameworkCore;
using SchedulingSystem.API.Data;
using SchedulingSystem.API.DTOs;
using SchedulingSystem.API.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SchedulingSystem.API.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public UserService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<User> RegisterAsync(RegisterRequestDto registerDto)
        {
            if (await EmailExistsAsync(registerDto.Email))
                throw new Exception("Email já está em uso");

            var user = new User
            {
                Name = registerDto.Name,
                Email = registerDto.Email,
                Password = registerDto.Password
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto loginDto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == loginDto.Email);

            if (user == null)
                throw new Exception("Email não cadastrado");

            if (user.Password != loginDto.Password)
                throw new Exception("Senha incorreta");

            var token = GenerateJwtToken(user);

            return new LoginResponseDto
            {
                Token = token,
                Message = "Login realizado com sucesso",
                User = new UserResponseDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email
                }
            };
        }

        public async Task<bool> UpdatePasswordAsync(UpdatePasswordRequestDto updateDto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == updateDto.Email);

            if (user == null)
                return false;

            if (user.Password != updateDto.CurrentPassword)
                return false;

            user.Password = updateDto.NewPassword;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Name)
            };

            var jwtKey = _configuration["Jwt:Key"] ?? throw new ArgumentNullException("Jwt:Key");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(8),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}