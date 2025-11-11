using Malvader.Models;

namespace Malvader.DTOs.RequestDTOs.Create
{
    public class CreateContaInvestimentoRequestDTO : CreateContaRequestDTO
    {
        public PerfilRisco PerfilRisco { get; set; }
        public decimal ValorMinimo { get; set; }
        public decimal TaxaRendimentoBase { get; set; }
    }
}
