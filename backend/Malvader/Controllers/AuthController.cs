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
                var loginData = _authService.LoginHandler(request);

                switch (loginData.TipoUsuario)
                {
                    case TipoUsuario.CLIENTE:
                        var clienteResponseDto = _usuarioService.GetClienteByUsuarioId(loginData.Id);
                        var responseDto = new LoginResponseDTO
                        {
                            Success = true,
                            Message = "Login realizado",    
                            Token = _jwtService.GenerateToken(loginData.CPF),
                            TipoUsuario = TipoUsuario.CLIENTE,
                            Data = clienteResponseDto
                        };
                        return Ok(responseDto);
                    case TipoUsuario.FUNCIONARIO:
                        var funcionarioResponseDto = _usuarioService.GetFuncionarioByUsuarioId(loginData.Id);
                        responseDto = new LoginResponseDTO
                        {
                            Token = _jwtService.GenerateToken(loginData.CPF),
                            TipoUsuario = TipoUsuario.FUNCIONARIO,
                            Data = funcionarioResponseDto
                        };
                        return Ok(responseDto); ;
                    default:
                        return BadRequest(new LoginResponseDTO
                        {
                            Success = false,
                            Message = "erro",
                            Token = null,
                            TipoUsuario = null,
                            Data = null
                        });
                }
            }
            catch (AuthenticationException ex)
            {
                return Unauthorized(new LoginResponseDTO
                {
                    Success = false,
                    Message = ex.Message,
                    Token = null,
                    TipoUsuario = null,
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new LoginResponseDTO
                {
                    Success = false,
                    Message = ex.Message,
                    Token = null,
                    TipoUsuario = null,
                    Data = null
                });
            }
        }
    }
}
