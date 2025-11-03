using Malvader.DAO;
using Malvader.DAOs;
using Malvader.DTOs;
using Malvader.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Malvader.Controllers
{
    [Route("api/usuario/funcionario")]
    [ApiController]
    public class FuncionarioController : ControllerBase
    {
        private readonly UsuarioDAO _usuarioDao;
        private readonly ClienteDAO _clienteDao;
        private readonly UsuarioService _usuarioService;

        public FuncionarioController(UsuarioDAO usuarioDao, ClienteDAO clienteDao, UsuarioService usuarioService)
        {
            _usuarioDao = usuarioDao;
            _clienteDao = clienteDao;
            _usuarioService = usuarioService;
        }


        [HttpPost("Cliente")]
        public ActionResult CriarFuncionario([FromBody] CreateClienteRequestDTO requestDto)
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
            var (usuario, errorResponse) = _usuarioService.CriarUsuario(requestDto, errors);
            if (usuario == null) return BadRequest(errorResponse);

            (var cliente, errorResponse) = _usuarioService.CriarCliente(requestDto, usuario, errors);
            if (cliente == null) return BadRequest(errorResponse);

            var responseDto = new CreateClienteResponseDTO
            {
                Id = cliente.Id,
                Nome = cliente.Usuario.Nome,
                Cpf = cliente.Usuario.CPF,
                Success = true,
                Message = "cliente criado com sucesso!",
                UsuarioId = cliente.Usuario.Id
            };

            return Ok(responseDto);
        }

    }
}
