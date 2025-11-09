namespace Malvader.Models
{
    public class ContaCorrente
    {
        public int Id { get; set; }
        public int ContaId { get; set; }
        public decimal Limite { get; set; }
        public DateTime DataVencimento { get; set; }
        public decimal TaxaManutencao { get; set; }
    }
}
