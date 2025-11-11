namespace Malvader.Models
{
    public enum TipoErro
    {
        CPF,
        Senha
    }
    public class LoginErrorResponse : ErrorResponse
    {
        public TipoErro TipoErro { get; set; }
    }
}
