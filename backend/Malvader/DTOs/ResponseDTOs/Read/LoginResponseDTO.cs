using Malvader.Models;

namespace Malvader.DTOs.ResponseDTOs.Read
{
    public class LoginResponseDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string? Token { get; set; }
        public TipoUsuario? TipoUsuario { get; set; }
        public object? Data { get; set; }
    }
}
