using SchedulingSystem.API.DTOs;
using SchedulingSystem.API.Models;

namespace SchedulingSystem.API.Services
{
    public interface IUserService
    {
        Task<User> RegisterAsync(RegisterDto registerDto);
        Task<string> LoginAsync(LoginDto loginDto);
        Task<bool> ForgotPasswordAsync(string email);
        Task<bool> ResetPasswordAsync(ResetPasswordDto resetDto);
        Task<User> GetUserByIdAsync(int id);
    }
}