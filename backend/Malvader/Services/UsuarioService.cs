using Malvader.DAO;
using Malvader.DAOs;
using Malvader.DTOs.RequestDTOs.Create;
using Malvader.DTOs.ResponseDTOs.Read;
using Malvader.Models;
using MySqlX.XDevAPI;

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
        #region Inserts
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
            novoCliente = _clienteDao.Insert(novoCliente);
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
            if (string.IsNullOrEmpty(requestDto.AgenciaId.ToString()))
            {
                errors.Add("Insira o id da agencia");
            }
            if (string.IsNullOrEmpty(requestDto.Cargo.ToString()))
            {
                errors.Add("Insira o cargo do funcionario");
            }

            if (errors.Any() || usuario == null)
            {
                errorResponse = new ErrorResponse { Errors = errors };
                return (null, null, errorResponse);
            }
            Console.WriteLine(requestDto.SupervisorId);
            var novoFuncionario = new Funcionario
            {
                UsuarioId = usuario.Id,
                AgenciaId = requestDto.AgenciaId,
                Cargo = Enum.Parse<Cargo>(requestDto.Cargo.ToString()),
                SupervisorId = requestDto.SupervisorId,
                CodigoFuncionario = requestDto.CodigoFuncionario
            };

            novoFuncionario = _funcionarioDao.Insert(novoFuncionario);
            return (novoFuncionario, usuario, null);
        }
        #endregion
        #region Fetches
        public (Cliente? cliente, Usuario? usuario, ErrorResponse? errorResponse) GetClienteById(int id)
        {
            var errors = new List<string>();
            Cliente? cliente = _clienteDao.GetById(id);
            if (cliente == null) return (null, null, new ErrorResponse { Errors = ["Não foi possível encontrar o cliente pelo ID informado"] });

            var (usuario, errorResponse) = GetUsuarioById(cliente.UsuarioId);
            if (usuario == null) return (null, null, errorResponse);

            return (cliente, usuario, null);
        }
        public (Funcionario? funcionario, Usuario? usuario, ErrorResponse? errorResponse) GetFuncionarioById(int id)
        {
            var errors = new List<string>();
            Funcionario? funcionario = _funcionarioDao.GetById(id);
            if (funcionario == null) return (null, null, new ErrorResponse { Errors = ["Não foi possível encontrar o funcionário pelo ID informado"] });
            var (usuario, errorResponse) = GetUsuarioById(funcionario.UsuarioId);
            if (usuario == null) return (null, null, errorResponse);

            return (funcionario, usuario, null);
        }
        #endregion
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
            if (string.IsNullOrEmpty(requestDTO.TipoUsuario.ToString()))
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
                TipoUsuario = Enum.Parse<TipoUsuario>(requestDTO.TipoUsuario.ToString()),
                SenhaHash = requestDTO.Senha
            };

            novoUsuario = _usuarioDao.Insert(novoUsuario);
            return (novoUsuario, null);
        }

        private (Usuario? usuario, ErrorResponse? errorResponse) GetUsuarioById(int id)
        {
            Usuario? usuario = _usuarioDao.GetById(id);
            if (usuario == null) return (null, new ErrorResponse { Errors = ["Não foi possível encontrar o usuario pelo ID informado"] });
            
            return (usuario, null);
        }
        #endregion
    }
}
