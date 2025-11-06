using Malvader.Models;

namespace Malvader.DTOs
{
    public class CreateClienteResponseDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public int UsuarioId { get; set; }
    }
}
