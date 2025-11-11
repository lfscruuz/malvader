using Malvader.DTOs.ResponseDTOs.Create;

namespace Malvader.DTOs.ResponseDTOs.Read
{
    public class ReadClienteResponseDTO: ReadUsuarioResponseDTO
    {
        public int Id { get; set; }
        public bool Success { get; set; }
        public required string Message { get; set; }
        public decimal ScoreCredito { get; set; }
    }
}
