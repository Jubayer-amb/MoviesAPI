namespace MoviesAPI.DTOs
{
    public class AuthenticationRespose
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
