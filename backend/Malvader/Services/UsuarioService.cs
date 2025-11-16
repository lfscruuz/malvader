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
        private readonly AgenciaService _agenciaService;

        public UsuarioService(UsuarioDAO usuarioDao, ClienteDAO clienteDao, FuncionarioDAO funcionario, AgenciaService agenciaService)
        {
            _usuarioDao = usuarioDao;
            _clienteDao = clienteDao;
            _funcionarioDao = funcionario;
            _agenciaService = agenciaService;
        }
        #region Public Methods
        #region Inserts
        public (Cliente cliente, Usuario usuario ) CreateCliente(CreateClienteRequestDTO requestDto)
        {
            var usuario = CreateUsuario(requestDto);

            if (string.IsNullOrEmpty(usuario?.Id.ToString()))
            {
                throw new ArgumentException("Insira o id do usuário");
            }

            var novoCliente = new Cliente
            {
                UsuarioId = usuario.Id
            };
            novoCliente = _clienteDao.Insert(novoCliente);
            return (novoCliente, usuario);
        }
        public (Funcionario funcionario, Usuario usuario) CreateFuncionario(
            CreateFuncionarioRequestDTO requestDto)
        {
            var errors = new List<string>();
            var usuario = CreateUsuario(requestDto);
            var (agencia, _) = _agenciaService.GetAgenciaIdByCodigo(requestDto.CodigoAgencia);

            if (string.IsNullOrEmpty(requestDto.CodigoAgencia))
            {
                errors.Add("Insira o código da agencia");
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
                AgenciaId = agencia.Id,
                Cargo = Enum.Parse<Cargo>(requestDto.Cargo.ToString()),
                CodigoFuncionario = requestDto.CodigoFuncionario
            };
            
            if (requestDto.CodigoSupervisor != null)
            {
                var supervisor = _funcionarioDao.GetByCodigo(requestDto.CodigoSupervisor);
                novoFuncionario.SupervisorId = supervisor.Id;
            }

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

        public Usuario GetUsuarioByCpf(string cpf)
        {
            var usuario = _usuarioDao.GetByCpf(cpf);
            return usuario;
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
