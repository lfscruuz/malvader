using Malvader.DTOs.RequestDTOs.Create;
using Malvader.Models;
using Malvader.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace Malvader.Controllers
{
    [Route("api/conta")]
    [ApiController]
    public class ContaController : ControllerBase
    {
        private readonly ContaService _contaService;

        public ContaController(ContaService contaService)
        {
            _contaService = contaService;
        }

        [HttpPost("")]
        public IActionResult NewConta([FromBody] CreateContaCorrenteRequestDTO requestDto)
        {
            //var (conta, errorResponse ) = _contaService.CreateConta(requestDto);
            //if (conta == null) return BadRequest(errorResponse);

            return Ok(requestDto);
           

        }
    }
}
