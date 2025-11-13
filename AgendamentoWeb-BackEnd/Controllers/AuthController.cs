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
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerDto)
        {
            try
            {
                var user = await _userService.RegisterAsync(registerDto);
                return Ok(new RegisterResponseDto
                {
                    Message = "Usuário registrado com sucesso",
                    User = new UserResponseDto
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Email = user.Email
                    }
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginDto)
        {
            try
            {
                var result = await _userService.LoginAsync(loginDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        [HttpPost("update-password")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordRequestDto updateDto) // CORRIGIDO
        {
            try
            {
                var result = await _userService.UpdatePasswordAsync(updateDto);
                if (result)
                    return Ok(new { message = "Senha atualizada com sucesso" });
                else
                    return BadRequest(new { message = "Senha atual incorreta ou usuário não encontrado" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}