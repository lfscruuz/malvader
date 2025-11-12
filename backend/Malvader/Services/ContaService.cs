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

            if (!Enum.IsDefined(typeof(TipoConta), requestDto.TipoConta))
            {
                throw new Exception("Tipo da conta é obrigatório\n");
            }

            var conta = CreateConta(requestDto);
            var contaCorrente = new ContaCorrente
            {
                ContaId = conta.Id,
                DataVencimento = requestDto.DataVencimento
            };
            contaCorrente = _contaCorrenteDao.Insert(contaCorrente);
            return contaCorrente;

        }
        public ContaPoupanca CreateContaPoupanca(CreateContaPoupancaRequestDTO requestDto)
        {
            var errors = new List<string>();
            if (requestDto.TaxaRendimento < 0)
            {
                errors.Add("taxa de rendimento é obrigatória");
            }
            if (string.IsNullOrEmpty(requestDto.UltimoRendimento.ToString()))
            {
                errors.Add("data do último rendimento é obrigatória");
            }

            var conta = CreateConta(requestDto);
            var contaPoupanca = new ContaPoupanca
            {
                ContaId = conta.Id,
                TaxaRendimento = requestDto.TaxaRendimento,
                UltimoRendimento = requestDto.UltimoRendimento
            };
            contaPoupanca = _contaPoupancaDao.Insert(contaPoupanca);
            return contaPoupanca;
        }
        public ContaInvestimento CreateContaInvestimento(CreateContaInvestimentoRequestDTO requestDto)
        {
            var errors = new List<string>();
            if (requestDto.TaxaRendimentoBase < 0)
            {
                errors.Add("Taxa de rendimento é obrigatória");
            }
            if (requestDto.ValorMinimo < 0)
            {
                errors.Add("Valor mínimo é obrigatória");
            }
            if (!Enum.IsDefined(typeof(PerfilRisco), requestDto.PerfilRisco))
            {
                errors.Add("Perfil de risco é obrigatória");
            }

            var conta = CreateConta(requestDto);
            var contaInvestimento = new ContaInvestimento
            {
                ContaId = conta.Id,
                PerfilRisco = requestDto.PerfilRisco,
                ValorMinimo = requestDto.ValorMinimo,
                TaxaRendimentoBase = requestDto.TaxaRendimentoBase
            };

            contaInvestimento = _contaInvestimentoDao.Insert(contaInvestimento);
            return contaInvestimento;
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
