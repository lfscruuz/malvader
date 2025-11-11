using System.Globalization;

namespace Malvader.Models
{
    public enum TipoConta
    {
        POUPANCA,
        CORRENTE,
        INVESTIMENTO
    }
    public enum StatusConta
    {
        ATIVA,
        ENCERRADA,
        BLOQUEADA
    }

    public class Conta
    {
        public int Id { get; set; }
        public string NumeroConta { get; set; }
        public int AgenciaId { get; set; }
        public decimal Saldo { get; set; }
        public TipoConta TipoConta {  get; set; }
        public int ClienteId { get; set; }
        public DateTime DataAbertura { get; set; }
        public StatusConta StatusConta {  get; set; }

    }
}
