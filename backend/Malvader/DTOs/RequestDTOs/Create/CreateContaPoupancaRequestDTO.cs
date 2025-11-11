namespace Malvader.DTOs.RequestDTOs.Create
{
    public class CreateContaPoupancaRequestDTO : CreateContaRequestDTO
    {
        public decimal TaxaRendimento { get; set; }
        public DateTime UltimoRendimento { get; set; }
    }
}
