namespace Malvader.DTOs.ResponseDTOs.Read
{
    public class ReadEnderecoAgenciaResponseDTO
    {
        public int Id { get; set; }
        public required string Cep { get; set; }
        public required string Local { get; set; }
        public int Numero { get; set; }
        public required string Bairro { get; set; }
        public required string Cidade { get; set; }
        public required string Estado { get; set; }
        public string? Complemento { get; set; }
    }
}
