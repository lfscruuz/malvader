namespace Malvader.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public decimal ScoreCredito { get; set; } = 0;
    }
}
