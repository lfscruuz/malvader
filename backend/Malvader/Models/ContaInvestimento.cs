namespace Malvader.Models
{
    public enum PerfilRisco
    {
        BAIXO,
        MEIDO,
        ALTO
    }
    public class ContaInvestimento
    {
        public int Id { get; set; }
        public int ContaId { get; set; }
        public PerfilRisco PerfilRisco {  get; set; }
        public decimal ValorMinimo { get; set; }
        public decimal TaxaRendimentoBase { get; set; }
    }
}
