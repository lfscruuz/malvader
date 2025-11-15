using Malvader.Models;

namespace Malvader.DTOs.RequestDTOs.Create
{
    public class CreateTransacaoResponseDTO
    {
        public string NumeroContaOrigem { get; set; }
        public string NumeroContaDestino { get; set; }
        public TipoTransacao TipoTransacao { get; set; }
        public decimal Valor { get; set; }
        public string Descricao { get; set; }
        public bool Success { get; set; }
        public string Message {  get; set; }
    }
}
