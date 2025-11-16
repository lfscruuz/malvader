using Malvader.DTOs.RequestDTOs.Create;
using Malvader.DTOs.RequestDTOs.Delete;
using Malvader.DTOs.RequestDTOs.Read;
using Malvader.DTOs.ResponseDTOs.Delete;
using Malvader.DTOs.ResponseDTOs.Read;
using Malvader.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mysqlx.Crud;
using System.Security.Policy;

namespace Malvader.Controllers
{
    [ApiController]
    [Route("api/conta")]
    public class ContaController : ControllerBase
    {
        protected readonly ContaService _contaService;
        protected readonly UsuarioService _usuarioService;
        protected readonly AuthService _authService;

        public ContaController(ContaService contaService, UsuarioService usuarioService, AuthService authService)
        {
            _contaService = contaService;
            _usuarioService = usuarioService;
            _authService = authService;
        }

        [HttpDelete]
        [Authorize]
        public IActionResult DeleteConta([FromBody] DeleteContaRequestDTO requestDto)
        {
            //curl - X DELETE "https://localhost:7108/api/conta" - H "accept: */*" - H "Content-Type: application/json" - H "Authorization: Bearer YOUR_TOKEN_HERE" - d "{\"numeroConta\": \"0040001125111520408\", \"motivo\": \"cansei\", \"senha\": \"#Abcd1234\"}"

            try
            {
                //pega cpf do jwt e verifica se senha bate
                var cpf = User.FindFirst("cpf")?.Value;
                _authService.CheckCPFandSenha(cpf, requestDto.Senha);

                _contaService.DeleteConta(requestDto, cpf);
                var responseDto = new DeleteContaResponseDto
                {
                    Success = true,
                    Message = "Conta fechada com sucesso!"
                };
                return Ok(responseDto);
            }
            catch (Exception ex)
            {
                var responseDto = new DeleteContaResponseDto
                {
                    Success = false,
                    Message = ex.Message,
                };
                return BadRequest(responseDto);
            }
        }

        [HttpPost("extrato")]
        [Authorize]
        public IActionResult Extrato([FromBody] ReadExtratoRequestDTO requestDto)
        {
            try
            {
                var extrato = _contaService.GetExtrato(requestDto);
                var responseDto = new ReadExtratoResponseDTO
                {
                    NumeroConta = extrato.NumeroConta,
                    SaldoAtual = extrato.SaldoAtual,
                    DataGeracao = extrato.DataGeracao,
                    Transacoes = extrato.Transacoes
                };
                return Ok(responseDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

    [ApiController]
    [Route("corrente")]
    public class ContaCorrenteController : ContaController
    {
        public ContaCorrenteController(ContaService contaService,
            UsuarioService usuarioService,
            AuthService authService) : base(contaService, usuarioService, authService) { }

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
    public class ContaInvestimentoController : ContaController
    {
        public ContaInvestimentoController(
            ContaService contaService,
            UsuarioService usuarioService,
            AuthService authService) : base(contaService, usuarioService, authService) { }

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
    public class ContaPoupancaController : ContaController
    {
        public ContaPoupancaController(
            ContaService contaService,
            UsuarioService usuarioService,
            AuthService authService) : base(contaService, usuarioService, authService) { }

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
