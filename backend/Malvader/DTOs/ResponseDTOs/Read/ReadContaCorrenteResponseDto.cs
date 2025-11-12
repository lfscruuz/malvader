namespace Malvader.DTOs.ResponseDTOs.Read
{
    public class ReadContaCorrenteResponseDto
    {
        public int Id { get; set; }
        public int ContaId { get; set; }
        public decimal Limite { get; set; }
        public DateTime DataVencimento { get; set; }
        public decimal TaxaManutencao { get; set; }
        public ReadContaResponseDTO Conta {  get; set; }
    }
}
