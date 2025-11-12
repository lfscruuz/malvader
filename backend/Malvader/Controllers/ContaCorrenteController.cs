using Malvader.DTOs.RequestDTOs.Create;
using Malvader.DTOs.ResponseDTOs.Read;
using Malvader.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Malvader.Controllers
{
    [Route("api/conta/corrente")]
    [ApiController]
    public class ContaCorrenteController : ControllerBase
    {
        private readonly ContaService _contaService;

        public ContaCorrenteController(ContaService contaService)
        {
            _contaService = contaService;
        }

        [HttpPost]
        public IActionResult NewContaCorrente([FromBody] CreateContaCorrenteRequestDTO requestDto)
        {
            //var (conta, errorResponse) = _contaService.CreateConta(requestDto);
            //if (conta == null) return BadRequest(errorResponse);

            try
            {
                var conta = _contaService.CreateContaCorrente(requestDto);
                return Created();
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
