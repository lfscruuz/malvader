using Malvader.DAO;
using Malvader.DTOs.RequestDTOs.Read;
using Malvader.DTOs.ResponseDTOs.Read;
using Malvader.Models;
using System.Security.Authentication;
using System.Security.Policy;

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

            CheckPasswords(loginRequest.Senha, usuario.SenhaHash);

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
        public void CheckPasswords(string senhaLogin, string senhaUsuario)
        {
            if (HashMD5(senhaLogin) != senhaUsuario) throw new AuthenticationException("CPF ou senha incorreto");
        }

        private string HashMD5(string input)
        {
            using var md5 = System.Security.Cryptography.MD5.Create();
            var bytes = System.Text.Encoding.UTF8.GetBytes(input);
            var hashBytes = md5.ComputeHash(bytes);
            return Convert.ToHexString(hashBytes).ToLower();
        }

        public void CheckCPFandSenha(string cpf, string senha)
        {
            var usuario = _usuarioService.GetUsuarioByCpf(cpf);
            CheckPasswords(senha, usuario.SenhaHash);
        }
    }
}
