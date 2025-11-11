using Malvader.Models;

namespace Malvader.DTOs.ResponseDTOs.Read
{
    public class ReadFuncionarioResponseDTO : ReadUsuarioResponseDTO
    {
        public int Id { get; set; }
        public bool Success { get; set; }
        public required string Message { get; set; }
        public int AgenciaId { get; set; }
        public string? CodigoFuncionario { get; set; }
        public Cargo Cargo { get; set; }
        public int? SupervisorId { get; set; }
    }
}
