using Malvader.DTOs.RequestDTOs.Create;
using Malvader.DTOs.ResponseDTOs.Create;
using Malvader.Services;
using Microsoft.AspNetCore.Mvc;

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
            var errors = new List<string>();
            var (agencia, endereco, errorResponse) = _agenciaService.CriarAgencia(requestDTO, errors);
            if (errorResponse != null) {
                return BadRequest(errorResponse);
            }
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
    }
}
