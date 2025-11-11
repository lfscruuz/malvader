using Malvader.Models;

namespace Malvader.DTOs.ResponseDTOs.Create
{
    public class CreateContaResponseDTO
    {
        public int Id { get; set; }
        public string NumeroConta { get; set; }
        public int AgenciaId { get; set; }
        public decimal Saldo { get; set; }
        public TipoConta TipoConta { get; set; }
        public int ClienteId { get; set; }
        public DateTime DataAbertura { get; set; }
        public StatusConta StatusConta { get; set; }
    }
}
