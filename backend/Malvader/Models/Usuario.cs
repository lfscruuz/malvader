namespace Malvader.Models
{
    public enum TipoUsuario
    {
        FUNCIONARIO,
        CLIENTE
    }

    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Telefone { get; set; }
        public TipoUsuario TipoUsuario {  get; set; }
        public string SenhaHash { get; set; }

    }
}
