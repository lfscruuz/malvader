using Malvader.Models;

namespace Malvader.DTOs.ResponseDTOs.Create
{
    public class CreateFuncionarioResponseDTO
    {
        public int Id { get; set; }
        public bool Success { get; set; }
        public required string Message { get; set; }
        public required CreateUsuarioResponseDTO Usuario { get; set; }
    }
}
