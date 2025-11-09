using Malvader.DAO;
using Malvader.DAOs;
using Malvader.DTOs.RequestDTOs.Create;
using Malvader.DTOs.ResponseDTOs.Create;
using Malvader.Models;
using Malvader.Services;
using Microsoft.AspNetCore.Mvc;

namespace Malvader.Controllers
{
    [Route("api/usuario")]
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

        [HttpPost("cliente")]
        public ActionResult NovoCliente([FromBody] CreateClienteRequestDTO requestDto)
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
            var (cliente, usuario, errorResponse) = _usuarioService.CriarCliente(requestDto, errors);
            if (cliente == null) return BadRequest(errorResponse);

            var usuarioResponseDto = new CreateUsuarioResponseDTO
            {
                Nome = usuario.Nome,
                CPF = usuario.CPF,
                DataNascimento = usuario.DataNascimento,
                Telefone = usuario.Telefone,
                TipoUsuario = usuario.Tipo
            };
            var clienteResponseDto = new CreateFuncionarioResponseDTO
            {
                Id = cliente.Id,
                Success = true,
                Message = "Funcionário criado com sucesso!",
                Usuario = usuarioResponseDto
            };

            return Ok(clienteResponseDto);
        }
    }
}
