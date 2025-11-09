namespace Malvader.Models
{
    public enum TipoTransacao
    {
        DEPOSITO,
        SAQUE,
        TRANSFERENCIA,
        TAXA,
        RENDIMENTO
    }
    public class Transacao
    {
        public int Id { get; set; }
        public int ContaOrigemId { get; set; }
        public int ContaDestinoId { get; set; }
        public TipoTransacao TipoTransacao { get; set; }
        public decimal Valor {  get; set; }
        public DateTime DataHora { get; set; }
        public string Descricao { get; set; }
    }
}
