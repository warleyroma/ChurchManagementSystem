namespace ControleDeAutenticaçao.Models
{
    public class AuthenticationResponse
    {
        public string Token { get; set; }
        public string Expiration { get; set; }
    }
}
