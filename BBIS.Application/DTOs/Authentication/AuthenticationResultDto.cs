using BBIS.Application.DTOs.UserAccount;

namespace BBIS.Application.DTOs.Authentication
{
    public class AuthenticationResultDto
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public bool Success { get; set; }
        public UserAccountViewDto UserInfo { get; set; }

        public AuthenticationResultDto()
        {
            Success = false;
        }
    }
}
