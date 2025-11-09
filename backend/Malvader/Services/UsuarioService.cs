using Malvader.DAO;
using Malvader.DAOs;
using Malvader.DTOs.RequestDTOs.Create;
using Malvader.Models;

namespace Malvader.Services
{
    public class UsuarioService
    {
        private readonly UsuarioDAO _usuarioDao;
        private readonly ClienteDAO _clienteDao;
        private readonly FuncionarioDAO _funcionarioDao;

        public UsuarioService(UsuarioDAO usuarioDao, ClienteDAO clienteDao, FuncionarioDAO funcionario)
        {
            _usuarioDao = usuarioDao;
            _clienteDao = clienteDao;
            _funcionarioDao = funcionario;
        }
        #region Public Methods
        public (Cliente? cliente, Usuario? usuario, ErrorResponse? errorResponse) CriarCliente(
            CreateClienteRequestDTO requestDto,
            List<string> errors)
        {
            var (usuario, errorResponse) = CriarUsuario(requestDto, errors);
            if (usuario == null) return (null, null, new ErrorResponse { Errors = errors });

            if (string.IsNullOrEmpty(usuario?.Id.ToString()))
            {
                errors.Add("Insira o id do usuário");
            }
            if (string.IsNullOrEmpty(requestDto.ScoreCredito.ToString()))
            {
                errors.Add("Insira o score");
            }

            if (errors.Any())
            {
                errorResponse = new ErrorResponse { Errors = errors };
                return (null, null, errorResponse);
            }

            var novoCliente = new Cliente
            {
                UsuarioId = usuario.Id,
                ScoreCredito = requestDto.ScoreCredito != 0 ? requestDto.ScoreCredito : 0,
            };
            novoCliente = _clienteDao.Inserir(novoCliente);
            return (novoCliente, usuario, null);
        }

        public (Funcionario? funcionario, Usuario? usuario, ErrorResponse? errorResponse) CriarFuncionario(
            CreateFuncionarioRequestDTO requestDto,
            List<string> errors)
        {
            var (usuario, errorResponse) = CriarUsuario(requestDto, errors);
            if (usuario == null) return (null, null, new ErrorResponse { Errors = errors });

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
                errorResponse = new ErrorResponse { Errors = errors };
                return (null, null, errorResponse);
            }

            var novoFuncionario = new Funcionario
            {
                UsuarioId = usuario.Id,
                AgenciaId = requestDto.Agencia.Id,
                Cargo = requestDto.Cargo,
                SupervisorId = requestDto.Supervisor.Id
            };

            novoFuncionario = _funcionarioDao.Inserir(novoFuncionario);
            return (novoFuncionario, usuario, null);
        }
        #endregion

        #region Private Methods
        private (Usuario? usuario, ErrorResponse? errorResponse) CriarUsuario(
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

        #endregion
    }
}
