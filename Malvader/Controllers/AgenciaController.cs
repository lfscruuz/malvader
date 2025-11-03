using Malvader.DTOs;
using Malvader.Models;
using Malvader.Services;
using Microsoft.AspNetCore.Http;
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
            var errors = new List<string>();
            var (novaAgencia, errorResponse) = _agenciaService.CriarAgencia(requestDTO, errors);
            if (errorResponse != null) {
                return BadRequest(errorResponse);
            }
            return Ok(novaAgencia);
        }
    }
}
