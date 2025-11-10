using Malvader.Models;

namespace Malvader.DTOs.ResponseDTOs.Read
{
    public class ReadUsuarioResponseDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Telefone { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
    }
}
