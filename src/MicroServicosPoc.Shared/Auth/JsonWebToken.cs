namespace MicroServicosPoc.Shared.Auth
{
    public class JsonWebToken
    {
        public string Token { get; set; }
        public long Expires { get; set; }        
    }
}