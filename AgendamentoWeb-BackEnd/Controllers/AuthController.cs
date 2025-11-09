using Microsoft.AspNetCore.Mvc;
using SchedulingSystem.API.DTOs;
using SchedulingSystem.API.Services;

namespace SchedulingSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                var user = await _userService.RegisterAsync(registerDto);
                return Ok(new { message = "Usuário registrado com sucesso", user.Id });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var token = await _userService.LoginAsync(loginDto);
                return Ok(new
                {
                    token,
                    message = "Login realizado com sucesso",
                    user = new { email = loginDto.Email }
                });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto forgotDto)
        {
            try
            {
                await _userService.ForgotPasswordAsync(forgotDto.Email);
                return Ok(new { message = "Instruções de recuperação enviadas para o email" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetDto)
        {
            try
            {
                var result = await _userService.ResetPasswordAsync(resetDto);
                if (result)
                    return Ok(new { message = "Senha redefinida com sucesso" });
                else
                    return BadRequest(new { message = "Token inválido ou expirado" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}