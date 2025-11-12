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

        public ContaCorrente CreateContaCorrente(CreateContaCorrenteRequestDTO requestDto)
        {

            if (string.IsNullOrEmpty(requestDto.TipoConta.ToString()))
            {
                throw new Exception("Tipo da conta é obrigatório\n");
            }

            var conta = CreateConta(requestDto);
            var contaCorrente = new ContaCorrente
            {
                ContaId = conta.Id,
                DataVencimento = requestDto.DataVencimento
            };
            contaCorrente = _contaCorrenteDao.Insert(contaCorrente, conta.Id);
            return contaCorrente;

        }
        private Conta CreateConta(CreateContaRequestDTO requestDto)
        {
            var errors = new List<string>();

            if (requestDto.AgenciaId <= 0)
                errors.Add("ID da agência é obrigatório.");

            if (!Enum.IsDefined(typeof(TipoConta), requestDto.TipoConta))
                errors.Add("Tipo da conta é obrigatório.");

            if (requestDto.ClienteId <= 0)
                errors.Add("ID do cliente é obrigatório.");

            if (errors.Any())
                throw new ArgumentException(string.Join("\n", errors));

            var conta = new Conta
            {
                AgenciaId = requestDto.AgenciaId,
                TipoConta = requestDto.TipoConta,
                ClienteId = requestDto.ClienteId,
                StatusConta = Enum.Parse<StatusConta>("ATIVA")
            };

            conta = _contaDao.Insert(conta);
            return conta;
        }
    }
}
