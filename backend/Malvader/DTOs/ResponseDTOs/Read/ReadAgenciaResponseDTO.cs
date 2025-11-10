using Malvader.DTOs.ResponseDTOs.Read;

namespace Malvader.DTOs.Read
{
    public class ReadAgenciaResponseDTO
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public string? CodigoAgencia { get; set; }
        public ReadEnderecoAgenciaResponseDTO? EnderecoAgencia { get; set; }
    }
}
