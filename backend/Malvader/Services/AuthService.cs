using Malvader.DAO;
using Malvader.DTOs.RequestDTOs.Read;
using Malvader.DTOs.ResponseDTOs.Read;
using Malvader.Models;

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

        public (ReadUsuarioResponseDTO?, ErrorResponse) LoginHandler(LoginRequestDTO loginRequest)
        {
            var errorResponse = new LoginErrorResponse();
            var usuario = _usuarioDao.GetByCpf(loginRequest.Cpf);
            if (usuario == null)
            {
                errorResponse.Errors.Add("Cpf não encontrado.");
                errorResponse.TipoErro = Enum.Parse<TipoErro>("CPF");

                return (null, errorResponse);
            }

            if (CheckPasswords(loginRequest, usuario) == false)
            {
                errorResponse.Errors.Add("Senha incorreta");
                errorResponse.TipoErro = Enum.Parse<TipoErro>("SENHA");
                return (null, errorResponse);
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
            return SelectUsuarioType(errorResponse, usuario, usuarioResponseDto);
        }

        private (ReadUsuarioResponseDTO?, ErrorResponse) SelectUsuarioType(ErrorResponse? errorResponse, Usuario usuario, ReadUsuarioResponseDTO usuarioResponseDto)
        {
            switch (usuarioResponseDto.TipoUsuario)
            {
                case TipoUsuario.CLIENTE:
                    (var clienteResponseDto, errorResponse) = _usuarioService.GetClienteByUsuarioId(usuario.Id);
                    if (clienteResponseDto == null)
                    {
                        errorResponse.Errors.Add("Não foi possível encontrar o cliente");
                        return (null, errorResponse);
                    }
                    return (clienteResponseDto, null);
                case TipoUsuario.FUNCIONARIO:
                    Console.WriteLine(usuario.Id);
                    (var funcionarioResponseDto, errorResponse) = _usuarioService.GetFuncionarioByUsuarioId(usuario.Id);
                    if (funcionarioResponseDto == null)
                    {
                        errorResponse.Errors.Add("Não foi possível encontrar o funcionário");
                        return (null, errorResponse);
                    }
                    return (funcionarioResponseDto, null);
                default:
                    return (usuarioResponseDto, null);
            }
        }

            private bool CheckPasswords(LoginRequestDTO loginRequest, Usuario usuario)
            {
                if (usuario.SenhaHash == loginRequest.Senha) return true;
                return false;
            }
    }
}
