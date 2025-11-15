using Malvader.DAO;
using Malvader.DAOs;
using Malvader.DTOs.RequestDTOs.Create;
using Malvader.DTOs.ResponseDTOs.Create;
using Malvader.DTOs.ResponseDTOs.Read;
using Malvader.Models;
using Malvader.Services;
using Microsoft.AspNetCore.Mvc;

namespace Malvader.Controllers
{
    [Route("api/usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        protected readonly UsuarioService _usuarioService;

        public UsuarioController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

       
    }

    [Route("cliente")]
    [ApiController]
    public class ClienteController : UsuarioController
    {
        public ClienteController(UsuarioService usuarioService) : base(usuarioService) { }
        [HttpPost]
        public ActionResult NewCliente([FromBody] CreateClienteRequestDTO requestDto)
        {
            /*{
                "nome": "luis",
                "cpf": "12312312312",
                  "dataNascimento": "2025-11-02",
                  "telefone": "(12) 1234-1234",
                  "tipoUsuario": "CLIENTE",
                  "senha": "123123",
                  "scoreCredito": 0
            }*/
            try
            {
                var errors = new List<string>();
                var (cliente, usuario, errorResponse) = _usuarioService.CreateCliente(requestDto, errors);
                if (cliente == null) return BadRequest(errorResponse);

                var usuarioResponseDto = new CreateUsuarioResponseDTO
                {
                    Nome = usuario.Nome,
                    CPF = usuario.CPF,
                    DataNascimento = usuario.DataNascimento,
                    Telefone = usuario.Telefone,
                    TipoUsuario = usuario.TipoUsuario
                };
                var clienteResponseDto = new CreateClienteResponseDTO
                {
                    Id = cliente.Id,
                    Success = true,
                    Message = "Funcionário criado com sucesso!",
                    Usuario = usuarioResponseDto
                };

                return Ok(clienteResponseDto);
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetCliente(int id)
        {
            try
            {

                var (cliente, usuario) = _usuarioService.GetClienteById(id);
                var usuarioResponseDto = new ReadUsuarioResponseDTO
                {
                    Id = usuario.Id,
                    Nome = usuario.Nome,
                    CPF = usuario.CPF,
                    DataNascimento = usuario.DataNascimento,
                    Telefone = usuario.Telefone,
                    TipoUsuario = usuario.TipoUsuario
                };
                var clienteResponseDto = new ReadClienteResponseDTO
                {
                    Id = cliente.Id,
                    Success = true,
                    Message = "query realizada com sucesso!"
                };

                return Ok(clienteResponseDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

    [Route("funcionario")]
    [ApiController]
    public class FuncionarioController : UsuarioController
    {
        public FuncionarioController(UsuarioService usuarioService) : base(usuarioService) { }

        [HttpPost]
        public ActionResult NewFuncionario([FromBody] CreateFuncionarioRequestDTO requestDto)
        {
            /*{
                "nome": "luis",
                "cpf": "12312312312",
                "dataNascimento": "2025-11-02",
                "telefone": "(12) 1234-1234",
                "tipoUsuario": "FUNCIONARIO",
                "senha": "123123",
                "agenciaId": 4,
                "cargo": "ESTAGIARIO",
                "codigoFuncionario" : "abc123",
                "supervisorId": 0
            }*/
            try
            {
                var (funcionario, usuario) = _usuarioService.CreateFuncionario(requestDto);

                var usuarioResponseDto = new CreateUsuarioResponseDTO
                {
                    Nome = usuario.Nome,
                    CPF = usuario.CPF,
                    DataNascimento = usuario.DataNascimento,
                    Telefone = usuario.Telefone,
                    TipoUsuario = usuario.TipoUsuario
                };
                var funcionarioResponseDto = new CreateFuncionarioResponseDTO
                {
                    Id = funcionario.Id,
                    Success = true,
                    Message = "Cliente criado com sucesso!",
                    CodigoFuncionario = funcionario.CodigoFuncionario,
                    Usuario = usuarioResponseDto
                };

                return Ok(funcionarioResponseDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetFuncionario(int id)
        {
            try
            {
                var (funcionario, usuario) = _usuarioService.GetFuncionarioById(id);
                var usuarioResponseDto = new ReadUsuarioResponseDTO
                {
                    Id = usuario.Id,
                    Nome = usuario.Nome,
                    CPF = usuario.CPF,
                    DataNascimento = usuario.DataNascimento,
                    Telefone = usuario.Telefone,
                    TipoUsuario = usuario.TipoUsuario
                };
                var funcionarioResponseDto = new ReadFuncionarioResponseDTO
                {
                    Id = funcionario.Id,
                    Success = true,
                    Message = "query realizada com sucesso!"
                };

                return Ok(funcionarioResponseDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
