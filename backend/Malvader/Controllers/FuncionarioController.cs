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
