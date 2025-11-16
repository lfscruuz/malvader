using Malvader.Models;

namespace Malvader.DTOs.ResponseDTOs.Read
{
    public class ReadExtratoResponseDTO
    {
        public string NumeroConta { get; set; }
        public decimal SaldoAtual { get; set; }

        public DateTime DataGeracao { get; set; }
        public List<Transacao> Transacoes { get; set; }
    }
}
