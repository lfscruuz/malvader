using Malvader.DTOs.RequestDTOs.Create;
using Malvader.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Malvader.Controllers
{
    [Route("api/transacao")]
    [ApiController]
    public class TransacaoController : ControllerBase
    {
        protected readonly TransacaoService _transacaoService;

        public TransacaoController(TransacaoService transacaoService)
        {
            _transacaoService = transacaoService;
        }
        [HttpPost]
        public IActionResult NewTransacao([FromBody] CreateTransacaoRequestDTO requetDto)
        {
            /* DEPOSITO
                {
                    "numeroContaDestino": "0030000725111215362",
                    "tipoTransacao": "DEPOSITO",
                    "valor": 150.34,
                    "descricao": "primeiro deposito"
                }
             */
            /* TRANSFERENCIA
                {
                  "numeroContaOrigem": "0030000725111215362",
                  "numeroContaDestino": "0030000825111215361",
                  "tipoTransacao": "TRANSFERENCIA",
                  "valor": 50,
                  "descricao": "transacao da conta x para y"
                }
             */
            /* SAQUE
                {
                    "numeroContaOrigem": "0030000725111215362",
                    "tipoTransacao": "SAQUE",
                    "valor": 25,
                    "descricao": "saque da conta y"
                }
             */
            try
            {
                var transacao = _transacaoService.CreateTransacao(requetDto);
                var responseDto = new CreateTransacaoResponseDTO
                {
                    NumeroContaOrigem = requetDto.NumeroContaOrigem,
                    NumeroContaDestino = requetDto.NumeroContaDestino,
                    TipoTransacao = transacao.TipoTransacao,
                    Valor = transacao.Valor,
                    Descricao = transacao.Descricao,
                    Success = true,
                    Message = "Transação efetuada com sucesso!"
                };
                return Ok(responseDto);

            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("deposito")]
        [ApiController]
        public class DepositoController : TransacaoController
        {
            public DepositoController(TransacaoService transacaoService) : base(transacaoService) { }


        }

        [Route("saque")]
        [ApiController]
        public class SaqueController : TransacaoController
        {
            public SaqueController(TransacaoService transacaoService) : base(transacaoService) { }
        }

        [Route("transferencia")]
        [ApiController]
        public class ApiController : TransacaoController
        {
            public ApiController(TransacaoService transacaoService) : base(transacaoService) { }
        }
    }
}
