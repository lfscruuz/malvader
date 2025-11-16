using Malvader.Models;

namespace Malvader.DTOs.ResponseDTOs.Read
{
    public class ReadTransacaoResponseDTO
    {
        public TipoTransacao Tipo { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataHora { get; set; }
        public string Descricao { get; set; }

        public int? ContaOrigemId { get; set; }
        public int? ContaDestinoId { get; set; }
    }
}
