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
    [Route("api/usuario/funcionario")]
    [ApiController]
    public class FuncionarioController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;

        public FuncionarioController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }


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
            var errors = new List<string>();
            var (funcionario, usuario, errorResponse) = _usuarioService.CriarFuncionario(requestDto, errors);
            if (funcionario == null) return BadRequest(errorResponse);

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

        [HttpGet("{id}")]
        public IActionResult GetFuncionario(int id)
        {
            var (funcionario, usuario, errorResponse) = _usuarioService.GetFuncionarioById(id);
            if (funcionario == null) return NotFound(errorResponse);
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
                Message = "query realizada com sucesso!",
                Usuario = usuarioResponseDto
            };

            return Ok(funcionarioResponseDto);
        }
        
    }
}
