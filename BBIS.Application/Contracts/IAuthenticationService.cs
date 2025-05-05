using BBIS.Application.DTOs.Authentication;

namespace BBIS.Application.Contracts
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResultDto> AuthenticateUser(LoginDto loginDto);
        Task<RefreshTokenResult> RefreshToken(RefreshTokenRequestDto dto);
    }
}
