using Malvader.DAOs;
using Malvader.DTOs.RequestDTOs.Create;
using Malvader.Models;
using ZstdSharp.Unsafe;

namespace Malvader.Services
{
    public class TransacaoService
    {
        private readonly TransacaoDAO _transacaoDao;
        private readonly ContaDAO _contaDao;

        public TransacaoService(TransacaoDAO transacaoDao, ContaDAO contaDAO)
        {
            _transacaoDao = transacaoDao;
            _contaDao = contaDAO;
        }

        public Transacao CreateTransacao(CreateTransacaoRequestDTO requestDto)
        {
            TipoTransacao tipoTransacao = requestDto.TipoTransacao;
            int? contaOrigemId = null;
            int? contaDestinoId = null;
            if (tipoTransacao == TipoTransacao.SAQUE)
            {
                if (requestDto.NumeroContaOrigem == null) throw new ArgumentException("Número da conta de origem é obrigatório");
                contaOrigemId = _contaDao.GetContaIdByNumeroConta(requestDto.NumeroContaOrigem);
            }

            if (tipoTransacao == TipoTransacao.DEPOSITO)
            {
                if (requestDto.NumeroContaDestino == null) throw new ArgumentException("Número da conta destino é obrigatório");
                contaDestinoId = _contaDao.GetContaIdByNumeroConta(requestDto.NumeroContaDestino);
            }

            if (tipoTransacao == TipoTransacao.TRANSFERENCIA) 
            {
                if (requestDto.NumeroContaOrigem == null) throw new ArgumentException("Número da conta destino é obrigatório");
                if (requestDto.NumeroContaDestino == null) throw new ArgumentException("Número da conta de origem é obrigatório");
                contaOrigemId = _contaDao.GetContaIdByNumeroConta(requestDto.NumeroContaOrigem);
                contaDestinoId = _contaDao.GetContaIdByNumeroConta(requestDto.NumeroContaDestino);
            }
            var transacao = new Transacao
            {
                ContaOrigemId = contaOrigemId,
                ContaDestinoId = contaDestinoId,
                TipoTransacao = tipoTransacao,
                Valor = requestDto.Valor,
                DataHora = DateTime.Now,
                Descricao = requestDto.Descricao
            };

            transacao = _transacaoDao.Insert(transacao);
            return transacao;
        }
    }
}
