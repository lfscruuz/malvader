using Malvader.Models;

namespace Malvader.DTOs.RequestDTOs.Create
{
    public class CreateTransacaoRequestDTO
    {
        public string? NumeroContaOrigem { get; set; }
        public string? NumeroContaDestino { get; set; }
        public TipoTransacao TipoTransacao { get; set; }
        public decimal Valor { get; set; }
        public string Descricao { get; set; }
    }
}
