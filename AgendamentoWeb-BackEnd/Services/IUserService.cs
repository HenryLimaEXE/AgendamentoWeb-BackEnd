using SchedulingSystem.API.DTOs;
using SchedulingSystem.API.Models;

namespace SchedulingSystem.API.Services
{
    public interface IUserService
    {
        Task<User> RegisterAsync(RegisterRequestDto registerDto);
        Task<LoginResponseDto> LoginAsync(LoginRequestDto loginDto);
        Task<bool> UpdatePasswordAsync(UpdatePasswordRequestDto updateDto);
        Task<User?> GetUserByIdAsync(int id);
        Task<bool> EmailExistsAsync(string email);
    }
}