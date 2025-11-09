using Malvader.DAO;
using Malvader.DAOs;
using Malvader.DTOs.RequestDTOs.Create;
using Malvader.DTOs.ResponseDTOs.Create;
using Malvader.Services;
using Microsoft.AspNetCore.Mvc;

namespace Malvader.Controllers
{
    [Route("api/usuario")]
    [ApiController]
    public class FuncionarioController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;

        public FuncionarioController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }


        [HttpPost("funcionario")]
        public ActionResult InserirFuncionario([FromBody] CreateFuncionarioRequestDTO requestDto)
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
            var (funcionario, usuario, errorResponse) = _usuarioService.CriarFuncionario(requestDto, errors);
            if (funcionario == null) return BadRequest(errorResponse);

            var usuarioResponseDto = new CreateUsuarioResponseDTO
            {
                Nome = usuario.Nome,
                CPF = usuario.CPF,
                DataNascimento = usuario.DataNascimento,
                Telefone = usuario.Telefone,
                TipoUsuario = usuario.Tipo
            };
            var funcionarioResponseDto = new CreateFuncionarioResponseDTO
            {
                Id = funcionario.Id,
                Success = true,
                Message = "Cliente criado com sucesso!",
                Usuario = usuarioResponseDto
            };

            return Ok(funcionarioResponseDto);
        }

    }
}
