using AutoMapper;
using BBIS.Application.Contracts;
using BBIS.Application.DTOs.Authentication;
using BBIS.Application.DTOs.UserAccount;
using BBIS.Common.Encryption;
using BBIS.Common.Enums;
using BBIS.Common.Exceptions;
using BBIS.Common.Jwt;
using BBIS.Domain.Contracts;
using BBIS.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BBIS.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IRepositoryWrapper repository;
        private readonly IEncryptionHandler encryptionHandler;
        private readonly IJwtHandler jwtHandler;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        public AuthenticationService(IRepositoryWrapper repository, IEncryptionHandler encryptionHandler, IJwtHandler jwtHandler, IMapper mapper, IConfiguration configuration)
        {
            this.repository = repository;
            this.encryptionHandler = encryptionHandler;
            this.jwtHandler = jwtHandler;
            this.mapper = mapper;
            this.configuration = configuration;
        }

        public async Task<AuthenticationResultDto> AuthenticateUser(LoginDto loginDto)
        {
            var userAccount = await repository.UserAccount.GetAuthUserAsync(loginDto.Username);

            if(userAccount == null)
            {
                return new AuthenticationResultDto();
            }

            if(!VerifyPassword(userAccount, loginDto.Password))
            {
                return new AuthenticationResultDto();
            }

            // generate token
            var token = CreateNewAccessToken(userAccount);
            var userInfo = mapper.Map<UserAccountViewDto>(userAccount);

            // create/update refresh token
            var refreshTokenValidity = GetRefreshtokenValidity();
            var refreshToken = await repository.UserAccount.GetRefreshToken(userAccount.UserAccountId);
            if (refreshToken == null)
            {
                refreshToken = new UserRefreshToken()
                {
                    UserAccountId = userAccount.UserAccountId,
                    RefreshToken = GenerateRefreshToken(),
                    RefreshTokenExpiration = DateTime.UtcNow.AddDays(refreshTokenValidity)
                };
            }
            else
            {
                refreshToken.RefreshToken = GenerateRefreshToken();
                refreshToken.RefreshTokenExpiration = DateTime.UtcNow.AddDays(refreshTokenValidity);
            }
            await repository.UserAccount.UpsertRefreshToken(refreshToken);

            return new AuthenticationResultDto
            {
                Token = token,
                RefreshToken = refreshToken.RefreshToken,
                Success = true,
                UserInfo = userInfo
            };
        }

        public async Task<RefreshTokenResult> RefreshToken(RefreshTokenRequestDto dto)
        {
            var result = new RefreshTokenResult();

            if (string.IsNullOrEmpty(dto.RefreshToken) || string.IsNullOrEmpty(dto.AccessToken)) 
            {
                throw new RefreshTokenException("Invalid client request");
            }

            var principal = GetPrincipalFromExpiredToken(dto.AccessToken);
            if (principal == null)
            {
                throw new RefreshTokenException("Invalid access token or refresh token");
            }

            string username = principal.Claims.First(c => c.Type == AuthClaimTypes.Username).Value;
         
            var user = await repository.UserAccount.GetAuthUserAsync(username);

            if (user == null || user.UserRefreshToken == null || user.UserRefreshToken.RefreshToken != dto.RefreshToken || user.UserRefreshToken.RefreshTokenExpiration <= DateTime.UtcNow)
            {
                throw new RefreshTokenException("Invalid access token or refresh token");
            }

            var newAccessToken = CreateNewAccessToken(user);
            var newRefreshToken = GenerateRefreshToken();

            var userRefreshToken = user.UserRefreshToken;
            userRefreshToken.RefreshToken = newRefreshToken;
            await repository.UserAccount.UpsertRefreshToken(userRefreshToken);

            return new RefreshTokenResult
            {
                RefreshToken = newRefreshToken,
                Token = newAccessToken,
                Success = true
            };
        }

        #region private methods

        private int GetRefreshtokenValidity()
        {
            _ = int.TryParse(configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);
            return refreshTokenValidityInDays;
        }

        private string CreateNewAccessToken(UserAccount user)
        {
            var fullName = $"{user.Firstname} {user.Lastname}";
            //var roles = user?.UserRoles?.Select(x => x.Role.RoleName).ToList();
            //var roles = user?.UserRoles?
            //.Where(x => x.UserAccountId == user.UserAccountId) // ensure only roles for this user
            //.Select(x => x.Role.RoleName)
            //.Distinct() // remove duplicates
            //.ToList();

            var roles = user?.UserRoles?
                 .Where(x => x.UserAccountId == user.UserAccountId && x.Role != null)
                 .SelectMany(x => x.Role.UserRoleScreeningAccesses ?? new List<UserRoleScreeningAccess>())
                 .Where(a => !string.IsNullOrEmpty(a.ScreeningTabName))
                 .Select(a => a.ScreeningTabName)
                 .Distinct()
                 .ToList() 
                 ?? 
                 new List<string>();

            // generate token
            var newAccessToken = this.jwtHandler.Create(user.UserAccountId, user.Username, fullName, roles);

            return newAccessToken;
        }

        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"])),
                ValidateLifetime = false
            };
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }

        private bool VerifyPassword(UserAccount user, string givenPassword)
        {
            var hashedGivenPassword = this.encryptionHandler.HashPassword(givenPassword, user.PasswordSalt);

            return hashedGivenPassword == user.Password;
        }

        #endregion
    }
}
