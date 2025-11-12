using Malvader.DAO;
using Malvader.DTOs.RequestDTOs.Read;
using Malvader.DTOs.ResponseDTOs.Read;
using Malvader.Models;
using System.Security.Authentication;

namespace Malvader.Services
{
    public class AuthService
    {
        private readonly UsuarioDAO _usuarioDao;
        private readonly UsuarioService _usuarioService;

        public AuthService(UsuarioDAO usuarioDao, UsuarioService usuarioService)
        {
            _usuarioDao = usuarioDao;
            _usuarioService = usuarioService;
        }

        public ReadUsuarioResponseDTO LoginHandler(LoginRequestDTO loginRequest)
        {
            var errorResponse = new LoginErrorResponse();
            var usuario = _usuarioDao.GetByCpf(loginRequest.Cpf);

            if (CheckPasswords(loginRequest, usuario) == false)
            {
                throw new AuthenticationException("CPF ou senha incorreto");
            }

            var usuarioResponseDto = new ReadUsuarioResponseDTO
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                CPF = usuario.CPF,
                DataNascimento = usuario.DataNascimento,
                Telefone = usuario.Telefone,
                TipoUsuario = usuario.TipoUsuario,
            };
            return usuarioResponseDto;
        }
        private bool CheckPasswords(LoginRequestDTO loginRequest, Usuario usuario)
        {
            if (usuario.SenhaHash == loginRequest.Senha) return true;
            return false;
        }
    }
}
