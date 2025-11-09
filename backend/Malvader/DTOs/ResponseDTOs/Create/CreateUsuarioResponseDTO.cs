using Malvader.Models;

namespace Malvader.DTOs.ResponseDTOs.Create
{
    public class CreateUsuarioResponseDTO
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Telefone { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
    }
}
