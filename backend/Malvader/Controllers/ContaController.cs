using Malvader.DTOs.RequestDTOs.Create;
using Malvader.DTOs.ResponseDTOs.Read;
using Malvader.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Malvader.Controllers
{
    [ApiController]
    [Route("api/conta")]
    public class ContaController(ContaService contaService) : ControllerBase
    {
        protected readonly ContaService _contaService = contaService;
    }

    [ApiController]
    [Route("corrente")]
    public class ContaCorrenteController(ContaService contaService) : ContaController(contaService)
    {
        [HttpPost]
        public IActionResult NewContaCorrente([FromBody] CreateContaCorrenteRequestDTO requestDto)
        {
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

    [ApiController]
    [Route("investimento")]
    public class ContaInvestimentoController(ContaService contaService) : ContaController(contaService)
    {
        [HttpPost]
        public IActionResult NewContaInvestimento([FromBody] CreateContaInvestimentoRequestDTO requestDto)
        {
            try
            {
                var conta = _contaService.CreateContaInvestimento(requestDto);
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

    [ApiController]
    [Route("api/conta/poupanca")]
    public class ContaPoupancaController(ContaService contaService) : ContaController(contaService)
    {
        [HttpPost]
        public IActionResult NewContaPoupanca([FromBody] CreateContaPoupancaRequestDTO requestDto)
        {
            try
            {
                var conta = _contaService.CreateContaPoupanca(requestDto);
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
