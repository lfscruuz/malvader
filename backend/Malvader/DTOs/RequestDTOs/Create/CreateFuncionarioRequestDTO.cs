using Malvader.Models;

namespace Malvader.DTOs.RequestDTOs.Create
{
    public class CreateFuncionarioRequestDTO : CreateUsuarioRequestDTO
    {
        public required string CodigoAgencia { get; set; }
        public Cargo Cargo { get; set; }
        public string? CodigoSupervisor { get; set; }
        public string? CodigoFuncionario { get; set; }
    }
}
