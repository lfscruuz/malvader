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
        public (Cliente? cliente, Usuario? usuario, ErrorResponse? errorResponse) CreateCliente(
            CreateClienteRequestDTO requestDto,
            List<string> errors)
        {
            var usuario = CreateUsuario(requestDto);
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
                throw new ArgumentException(string.Join("\n", errors));
            }

            var novoCliente = new Cliente
            {
                UsuarioId = usuario.Id,
                ScoreCredito = requestDto.ScoreCredito != 0 ? requestDto.ScoreCredito : 0,
            };
            novoCliente = _clienteDao.Insert(novoCliente);
            return (novoCliente, usuario, null);
        }
        public (Funcionario? funcionario, Usuario? usuario) CreateFuncionario(
            CreateFuncionarioRequestDTO requestDto)
        {
            var errors = new List<string>();
            var usuario = CreateUsuario(requestDto);

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

            if (errors.Any())
            {
                throw new ArgumentException(string.Join("\n", errors));
            }

            var novoFuncionario = new Funcionario
            {
                UsuarioId = usuario.Id,
                AgenciaId = requestDto.AgenciaId,
                Cargo = Enum.Parse<Cargo>(requestDto.Cargo.ToString()),
                SupervisorId = requestDto.SupervisorId,
                CodigoFuncionario = requestDto.CodigoFuncionario
            };

            novoFuncionario = _funcionarioDao.Insert(novoFuncionario);
            return (novoFuncionario, usuario);
        }
        #endregion
        #region Fetches
        public (Cliente? cliente, Usuario? usuario) GetClienteById(int id)
        {
            var cliente = _clienteDao.GetById(id);
            var usuario = GetUsuarioById(cliente.UsuarioId);

            return (cliente, usuario);
        }
        public ReadClienteResponseDTO GetClienteByUsuarioId(int id)
        {
            var cliente = _clienteDao.GetByUsuarioId(id);

            var responseDto = new ReadClienteResponseDTO
            {
                Id = cliente.Id,
                Success = true,
                Message = "Cliente requisitado com sucesso!",
                ScoreCredito = cliente.ScoreCredito
            };
            return (responseDto);
        }
        public ReadFuncionarioResponseDTO GetFuncionarioByUsuarioId(int id)
        {
            var funcionario = _funcionarioDao.GetByUsuarioId(id);
            var responseDto = new ReadFuncionarioResponseDTO
            {
                Id = funcionario.Id,
                Success = true,
                Message = "Cliente requisitado com sucesso!",
                AgenciaId = funcionario.AgenciaId,
                CodigoFuncionario = funcionario.CodigoFuncionario,
                Cargo = funcionario.Cargo,
                SupervisorId = funcionario.SupervisorId
                
            };
            return responseDto;
        }
        public (Funcionario? funcionario, Usuario? usuario) GetFuncionarioById(int id)
        {
            var funcionario = _funcionarioDao.GetById(id);
            var usuario = GetUsuarioById(funcionario.UsuarioId);

            return (funcionario, usuario);
        }
        #endregion
        #endregion

        #region Private Methods
        private Usuario CreateUsuario(
           CreateUsuarioRequestDTO requestDTO)
        {
            var errors = new List<string>();
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
                throw new ArgumentException(string.Join("\n", errors));
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
            return novoUsuario;
        }

        private Usuario GetUsuarioById(int id)
        {
            Usuario usuario = _usuarioDao.GetById(id);
            return usuario;
        }
        #endregion
    }
}
