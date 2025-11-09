namespace Malvader.Models
{
    public class ContaPoupanca
    {
        public int Id { get; set; }
        public int ContaId { get; set; }
        public decimal TaxaRendimento { get; set; }
        public DateTime UltimoRendimento { get; set; }
    }
}
