using Malvader.DTOs.RequestDTOs.Read;
using Malvader.Models;
using Malvader.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;

namespace Malvader.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService;
        private readonly AuthService _authService;

        public AuthController(JwtService jwtService, AuthService authService)
        {
            _jwtService = jwtService;
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDTO request)
        {
            var (responseDto, errorResponse ) = _authService.LoginHandler(request);
            if (errorResponse != null)
            {
                if (errorResponse is LoginErrorResponse) return Unauthorized("CPF ou Senha incorretos");
                return BadRequest(errorResponse);
            }
            return Ok(responseDto);
        }
    }
}
