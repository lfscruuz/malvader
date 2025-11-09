namespace Malvader.Models
{
    public class AuditoriaAberturaConta
    {
        public int Id { get; set; }
        public int ContaId { get; set; }
        public int FuncionarioId { get; set; }
        public DateTime DataHora { get; set; }
        public string Observacao { get; set; }
    }
}
