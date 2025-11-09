namespace Malvader.Models
{
    public class Relatorio
    {
        public int Id { get; set; }
        public int FuncionarioId { get; set; }
        public string TipoRelatorio { get; set; }
        public DateTime DataGeracao { get; set; }
        public string Conteudo { get; set; } = "{}";
    }
}
