namespace Malvader.DTOs.RequestDTOs.Read
{
    public class ReadExtratoRequestDTO
    {
        public string NumeroConta { get; set; }
        public int Limite { get; set; } = 10;
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
    }
}
