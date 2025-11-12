using Malvader.DTOs.RequestDTOs.Read;
using Malvader.DTOs.ResponseDTOs.Read;
using Malvader.Models;
using Malvader.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using System.Security.Authentication;

namespace Malvader.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService;
        private readonly AuthService _authService;
        private readonly UsuarioService _usuarioService;

        public AuthController(JwtService jwtService, AuthService authService, UsuarioService usuarioService)
        {
            _jwtService = jwtService;
            _authService = authService;
            _usuarioService = usuarioService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDTO request)
        {
            try
            {
                var responseDto = _authService.LoginHandler(request);

                switch (responseDto.TipoUsuario)
                {
                    case TipoUsuario.CLIENTE:
                        var clienteResponseDto = _usuarioService.GetClienteByUsuarioId(responseDto.Id);
                        return Ok(clienteResponseDto);
                    case TipoUsuario.FUNCIONARIO:
                        var funcionarioResponseDto = _usuarioService.GetFuncionarioByUsuarioId(responseDto.Id);
                        return Ok(funcionarioResponseDto);
                    default:
                        return BadRequest();
                }
            }
            catch (AuthenticationException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
