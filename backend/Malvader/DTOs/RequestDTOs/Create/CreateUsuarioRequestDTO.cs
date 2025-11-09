namespace Malvader.DTOs.RequestDTOs.Create
{
    public class CreateUsuarioRequestDTO
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Telefone { get; set; }
        public string TipoUsuario { get; set; }
        public string Senha { get; set; }
    }
}
