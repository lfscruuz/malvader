using Malvader.Models;

namespace Malvader.DTOs.RequestDTOs.Create
{
    public class CreateClienteRequestDTO : CreateUsuarioRequestDTO
    {
        public decimal ScoreCredito { get; set; }
    }
}
