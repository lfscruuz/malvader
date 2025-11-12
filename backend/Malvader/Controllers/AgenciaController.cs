using Malvader.DTOs.Read;
using Malvader.DTOs.RequestDTOs.Create;
using Malvader.DTOs.ResponseDTOs.Create;
using Malvader.DTOs.ResponseDTOs.Read;
using Malvader.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace Malvader.Controllers
{
    [Route("api/agencia")]
    [ApiController]
    public class AgenciaController : ControllerBase
    {
        private readonly AgenciaService _agenciaService;

        public AgenciaController(AgenciaService agenciaService)
        {
            _agenciaService = agenciaService;
        }

        [HttpPost]
        public IActionResult NovaAgencia([FromBody] CreateAgenciaRequestDTO requestDTO)
        {
            /*{
                "nome": "sjaalksj",
                "codigoAgencia": "string",
                "cep": "71929720",
                "local": "local1",
                "numero": 42,
                "bairro": "bairro1",
                "cidade": "brasilia",
                "estado": "DF",
                "complemento": "complemento1"
            }*/
            try
            {
                var (agencia, endereco) = _agenciaService.CreateAgencia(requestDTO);

                var agenciaResponseDto = new CreateAgenciaResponseDTO
                {
                    Id = agencia.Id,
                    Nome = agencia.Nome,
                    CodigoAgencia = agencia.CodigoAgencia,
                    Cep = endereco.Cep,
                    Local = endereco.Local,
                    Numero = endereco.Numero,
                    Bairro = endereco.Bairro,
                    Cidade = endereco.Cidade,
                    Estado = endereco.Estado,
                    Complemento = endereco.Complemento,
                };
                return Ok(agenciaResponseDto);
            }
            catch (ArgumentException ex) 
            { 
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetAgencia(int id)
        {
            try
            {
                var (agencia, enderecoAgencia) = _agenciaService.GetById(id);
                var enderecoAgenciaResponseDto = new ReadEnderecoAgenciaResponseDTO
                {
                    Id = enderecoAgencia.Id,
                    Cep = enderecoAgencia.Cep,
                    Local = enderecoAgencia.Local,
                    Numero = enderecoAgencia.Numero,
                    Bairro = enderecoAgencia.Bairro,
                    Cidade = enderecoAgencia.Cidade,
                    Estado = enderecoAgencia.Estado,
                    Complemento = enderecoAgencia.Complemento
                };
                var agenciaResponseDto = new ReadAgenciaResponseDTO
                {
                    Id = agencia.Id,
                    Nome = agencia.Nome,
                    CodigoAgencia = agencia.CodigoAgencia,
                    EnderecoAgencia = enderecoAgenciaResponseDto
                };

                return Ok(agenciaResponseDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
