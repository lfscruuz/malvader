using Malvader.DAOs;
using Malvader.DTOs.RequestDTOs.Create;
using Malvader.Models;

namespace Malvader.Services
{
    public class ContaService
    {
        private readonly ContaDAO _contaDao;
        private readonly ContaCorrenteDAO _contaCorrenteDao;
        private readonly ContaInvestimentoDAO _contaInvestimentoDao;
        private readonly ContaPoupancaDAO _contaPoupancaDao;

        public ContaService(
            ContaDAO contaDao,
            ContaCorrenteDAO contaCorrenteDao,
            ContaInvestimentoDAO contaInvestimentoDao,
            ContaPoupancaDAO contaPoupancaDao)
        {
            _contaDao = contaDao;
            _contaCorrenteDao = contaCorrenteDao;
            _contaInvestimentoDao = contaInvestimentoDao;
            _contaPoupancaDao = contaPoupancaDao;
        }

        //public (ContaCorrente? conta, ErrorResponse? errorResponse) CreateConta(CreateContaCorrenteRequestDTO requestDto)
        //{
        //    var errorResponse = new ErrorResponse();
        //    (var conta, errorResponse) = CreateConta(requestDto, errorResponse);

        //    if (errorResponse != null) return (null, errorResponse);

        //    if (string.IsNullOrEmpty(requestDto.TipoConta.ToString()))
        //    {
        //        errorResponse.Errors.Add("Tipo da conta é obrigatório");
        //    }
        //}
        private (Conta? conta, ErrorResponse? errorResponse) CreateConta(CreateContaRequestDTO requestDto, ErrorResponse errorResponse)
        {

            if (string.IsNullOrEmpty(requestDto.AgenciaId.ToString()))
            {
                errorResponse.Errors.Add("ID da agência é obrigatório");
            }
            if (string.IsNullOrEmpty(requestDto.TipoConta.ToString()))
            {
                errorResponse.Errors.Add("Tipo da conta é obrigatório");
            }
            if (string.IsNullOrEmpty(requestDto.ClienteId.ToString()))
            {
                errorResponse.Errors.Add("ID do clinete é obrigatório");
            }

            if (errorResponse.Errors.Any())
            {
                return (null, errorResponse);
            }

            var conta = new Conta
            {
                AgenciaId = requestDto.AgenciaId,
                TipoConta = requestDto.TipoConta,
                ClienteId = requestDto.ClienteId,
                StatusConta = Enum.Parse<StatusConta>("ATIVA")
            };

            conta = _contaDao.Insert(conta);
            if (conta == null)
            {
                errorResponse.Errors.Add("Não foi possível criar a conta");
                return (null, errorResponse);
            }

            return (conta, null);
        }
    }
}
