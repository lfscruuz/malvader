using Malvader.Models;

namespace Malvader.DTOs
{
    public class CreateFuncionarioRequestDTO : CreateUsuarioRequestDTO
    {
        public Agencia Agencia { get; set; }
        public int Id { get; set; }
        public Cargo Cargo { get; set; }
        public Funcionario? Supervisor { get; set; }
    }
}
