using Malvader.Models;

namespace Malvader.DTOs.RequestDTOs.Create
{
    public class CreateContaRequestDTO
    {
        public int AgenciaId { get; set; }
        public TipoConta TipoConta { get; set; }
        public int ClienteId { get; set; }
    }
}
