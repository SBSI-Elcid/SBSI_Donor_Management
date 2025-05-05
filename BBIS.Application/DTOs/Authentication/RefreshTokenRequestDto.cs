namespace BBIS.Application.DTOs.Authentication
{
    public class RefreshTokenRequestDto
    {
        public string RefreshToken { get; set; }
        public string AccessToken { get; set; }
    }
}
