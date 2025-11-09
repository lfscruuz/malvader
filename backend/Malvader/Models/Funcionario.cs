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
        public int UsuarioId { get; set; }
        public int AgenciaId { get; set; }
        public string? CodigoFuncionario { get; set; }
        public Cargo Cargo { get; set; }
        public int SupervisorId { get; set; }
    }
}
