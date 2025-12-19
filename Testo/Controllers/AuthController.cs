using ApplicationLayer.DTOs;
using ApplicationLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Task_API.Controllers
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

            [HttpPost("login")]
            public async Task<IActionResult> Login(LoginDto dto)
            {
                var result = await _authService.LoginAsync(dto);

                if (!result.Success)
                    return Unauthorized(new { message = result.Message });

                return Ok(new
                {
                    Success=true,
                    token = result.Token,
                    fullName = result.FullName,
                    role = result.Role
                });
            }
        }
    }
