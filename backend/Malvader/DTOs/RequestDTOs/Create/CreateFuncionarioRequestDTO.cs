using Malvader.Models;

namespace Malvader.DTOs.RequestDTOs.Create
{
    public class CreateFuncionarioRequestDTO : CreateUsuarioRequestDTO
    {
        public required int AgenciaId { get; set; }
        public Cargo Cargo { get; set; }
        public int? SupervisorId { get; set; }
        public string? CodigoFuncionario { get; set; }
    }
}
