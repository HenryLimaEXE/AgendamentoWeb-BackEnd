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
                return Ok(new
                {
                    message = "Usuário registrado com sucesso",
                    user = new { user.Id, user.Name, user.Email }
                });
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
                var result = await _userService.LoginAsync(loginDto);
                return Ok(new
                {
                    token = result.Token,
                    message = "Login realizado com sucesso",
                    user = result.User
                });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        [HttpPost("update-password")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordDto updateDto)
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