using Malvader.DAO;
using Malvader.DAOs;
using Malvader.DTOs;
using Malvader.Models;

namespace Malvader.Services
{
    public class UsuarioService
    {
        private readonly UsuarioDAO _usuarioDao;
        private readonly ClienteDAO _clienteDao;

        public UsuarioService(UsuarioDAO usuarioDao, ClienteDAO clienteDao)
        {
            _usuarioDao = usuarioDao;
            _clienteDao = clienteDao;
        }

        public (Usuario? usuario, ErrorResponse? errorResponse) CriarUsuario(
            CreateUsuarioRequestDTO requestDTO,
            List<string> errors)
        {
            if (string.IsNullOrEmpty(requestDTO.Nome))
            {
                errors.Add("Preencha o nome");
            }
            if (string.IsNullOrEmpty(requestDTO.Telefone))
            {
                errors.Add("Preencha o telefone");
            }
            if (string.IsNullOrEmpty(requestDTO.CPF))
            {
                errors.Add("Preencha o CPF");
            }
            if (string.IsNullOrEmpty(requestDTO.Senha))
            {
                errors.Add("Preencha a senha");
            }
            if (string.IsNullOrEmpty(requestDTO.DataNascimento.ToString()))
            {
                errors.Add("Preencha a data de nasvimento");
            }
            if (string.IsNullOrEmpty(requestDTO.TipoUsuario))
            {
                errors.Add("Preencha o tipo de usuário");
            }

            if (errors.Any())
            {
                var errorResponse = new ErrorResponse { Errors = errors };
                return (null, errorResponse);
            }
            var novoUsuario = new Usuario
            {
                Nome = requestDTO.Nome,
                Telefone = requestDTO.Telefone,
                CPF = requestDTO.CPF,
                DataNascimento = requestDTO.DataNascimento,
                Tipo = Enum.Parse<TipoUsuario>(requestDTO.TipoUsuario),
                SenhaHash = requestDTO.Senha
            };

            novoUsuario = _usuarioDao.Inserir(novoUsuario);
            return (novoUsuario, null);
        }
        public (Cliente? cliente, ErrorResponse? errorResponse) CriarCliente(
            CreateClienteRequestDTO requestDto,
            Usuario? usuario,
            List<string> errors)
        {
            if (string.IsNullOrEmpty(usuario?.Id.ToString()))
            {
                errors.Add("Insira o id do usuário");
            }
            if (string.IsNullOrEmpty(requestDto.ScoreCredito.ToString()))
            {
                errors.Add("Insira o score");
            }

            if (errors.Any() || usuario == null)
            {
                var errorResponse = new ErrorResponse { Errors = errors };
                return (null, errorResponse);
            }

            var novoCliente = new Cliente
            {
                Usuario = usuario,
                ScoreCredito = requestDto.ScoreCredito != 0 ? requestDto.ScoreCredito : 0,
            };
            novoCliente = _clienteDao.Inserir(novoCliente);
            return (novoCliente, null);
        }

        public (Funcionario? funcionario, ErrorResponse? errorResponse) CriarFuncionario(
            CreateFuncionarioRequestDTO requestDto,
            Usuario? usuario,
            List<string> errors)
        {
            if (string.IsNullOrEmpty(usuario?.Id.ToString()))
            {
                errors.Add("Insira o id do usuário");
            }
            if (string.IsNullOrEmpty(requestDto.Agencia.ToString()))
            {
                errors.Add("Insira o id da agencia");
            }

            if (errors.Any() || usuario == null)
            {
                var errorResponse = new ErrorResponse { Errors = errors };
                return (null, errorResponse);
            }

            var novoFuncionario = new Funcionario
            {
                Usuario = usuario,
                
            };

            return (novoFuncionario, null);
        }
    }
}
