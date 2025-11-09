using Malvader.Models;

namespace Malvader.DTOs.RequestDTOs.Create
{
    public class CreateFuncionarioRequestDTO : CreateUsuarioRequestDTO
    {
        public required Agencia Agencia { get; set; }
        public Cargo Cargo { get; set; }
        public Funcionario? Supervisor { get; set; }
    }
}
