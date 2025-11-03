namespace Malvader.Models
{
    public enum Cargo
    {
        ESTAGIARIO,
        ATENDENTE,
        GERENTE
    }

    public class Funcionario
    {
        public int Id { get; set; }
        public Usuario Usuario { get; set; }
        public Agencia Agencia { get; set; }
        public string CodigoFuncionario { get; set; }
        public Cargo Cargo { get; set; }
        public Funcionario Supervisor { get; set; }
    }
}
