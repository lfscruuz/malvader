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
    [Route("api/usuario/cliente")]
    [ApiController]
    public class ClienteCOntroller : ControllerBase
    {
        private readonly UsuarioDAO _usuarioDao;
        private readonly ClienteDAO _clienteDao;
        private readonly UsuarioService _usuarioService;

        public ClienteCOntroller(UsuarioDAO usuarioDao, ClienteDAO clienteDao, UsuarioService usuarioService)
        {
            _usuarioDao = usuarioDao;
            _clienteDao = clienteDao;
            _usuarioService = usuarioService;
        }

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
        }

        [HttpGet("{id}")]
        public IActionResult GetCliente(int id)
        {
            var (cliente, usuario, errorResponse) = _usuarioService.GetClienteById(id);
            if (cliente == null) return NotFound(errorResponse);
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
                Message = "query realizada com sucesso!",
                Usuario = usuarioResponseDto
            };

            return Ok(clienteResponseDto);
        }
    }
}
