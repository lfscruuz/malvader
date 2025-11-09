using System.Security.Policy;

namespace Malvader.Models
{
    public class EnderecoUsuario
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string Cep {  get; set; }
        public string Local { get; set; }
        public int NumeroCasa { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Complemento { get; set; }
    }
}
