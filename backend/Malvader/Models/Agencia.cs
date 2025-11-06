namespace Malvader.Models
{
    public class Agencia
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public string? CodigoAgencia { get; set; }
        public required EnderecoAgencia EnderecoAgencia { get; set; }

    }
}
