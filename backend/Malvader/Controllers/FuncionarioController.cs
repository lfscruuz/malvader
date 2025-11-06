using Malvader.DAO;
using Malvader.DAOs;
using Malvader.DTOs;
using Malvader.Services;
using Microsoft.AspNetCore.Mvc;

namespace Malvader.Controllers
{
    [Route("api/usuario/funcionario")]
    [ApiController]
    public class FuncionarioController : ControllerBase
    {
        private readonly UsuarioDAO _usuarioDao;
        private readonly FuncionarioDAO _funcionarioDao;
        private readonly UsuarioService _usuarioService;

        public FuncionarioController(UsuarioDAO usuarioDao, UsuarioService usuarioService, FuncionarioDAO funcionarioDao)
        {
            _usuarioDao = usuarioDao;
            _usuarioService = usuarioService;
            _funcionarioDao = funcionarioDao;
        }


        [HttpPost("Cliente")]
        public ActionResult CriarFuncionario([FromBody] CreateFuncionarioRequestDTO requestDto)
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

            (var funcionario, errorResponse) = _usuarioService.CriarFuncionario(requestDto, usuario, errors);
            if (funcionario == null) return BadRequest(errorResponse);

            var responseDto = new CreateClienteResponseDTO
            {
                Id = funcionario.Id,
                Nome = funcionario.Usuario.Nome,
                Cpf = funcionario.Usuario.CPF,
                Success = true,
                Message = "cliente criado com sucesso!",
                UsuarioId = funcionario.Usuario.Id
            };

            return Ok(responseDto);
        }

    }
}
