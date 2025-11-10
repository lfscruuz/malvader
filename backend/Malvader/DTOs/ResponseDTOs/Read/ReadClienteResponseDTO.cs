using Malvader.DTOs.ResponseDTOs.Create;

namespace Malvader.DTOs.ResponseDTOs.Read
{
    public class ReadClienteResponseDTO
    {
        public int Id { get; set; }
        public bool Success { get; set; }
        public required string Message { get; set; }
        public required ReadUsuarioResponseDTO Usuario { get; set; }
    }
}
