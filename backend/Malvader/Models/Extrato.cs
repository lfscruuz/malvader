namespace Malvader.Models
{
    public class Extrato
    {
        public string NumeroConta { get; set; }
        public decimal SaldoAtual { get; set; }

        public DateTime DataGeracao { get; set; } = DateTime.Now;

        public List<Transacao> Transacoes { get; set; }
    }
}
