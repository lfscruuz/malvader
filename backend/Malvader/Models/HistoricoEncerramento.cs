namespace Malvader.Models
{
    public class HistoricoEncerramento
    {
        public int Id { get; set; }
        public int ContaId { get; set; }
        public string Motivo { get; set; }
        public DateTime DataHora { get; set; }
    }
}
