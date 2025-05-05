namespace BBIS.Application.DTOs.Authentication
{
    public class RefreshTokenResult
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public bool Success { get; set; }

        public RefreshTokenResult()
        {
            Success = false;
        }
    }
}
