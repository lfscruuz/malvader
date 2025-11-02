using Malvader.Models;

namespace Malvader.DTOs
{
    public class CreateClienteRequestDTO : CreateUsuarioRequestDTO
    {
        public decimal ScoreCredito { get; set; }
    }
}
