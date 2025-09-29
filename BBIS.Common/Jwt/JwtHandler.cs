using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using BBIS.Common.Enums;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.IO;
using System.Linq;

namespace BBIS.Common.Jwt
{
    public class JwtHandler : IJwtHandler
    {
        private readonly int _tokenExpirationMinutes;
        private SigningCredentials _signingCredentials;
        private JwtHeader _jwtHeader;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        private string _issuer;
        private string _audience;

        public TokenValidationParameters Parameters { get; private set; }
        
        public JwtHandler(IConfiguration configuration)
        {
            _tokenExpirationMinutes = Convert.ToInt32(configuration["JWT:TokenExpirationMinutes"]);

            this.InitJwt(configuration);
        }
               
        public string Create(Guid userId, string username, string fullName, List<string> roles, List<string> roleaccess)
        {
            var nowUtc = DateTime.UtcNow;
            var expires = nowUtc.AddMinutes(_tokenExpirationMinutes);

            var claims = new List<Claim>
            {
                new Claim(AuthClaimTypes.UserId, $"{userId}"),
                new Claim(AuthClaimTypes.Name, $"{fullName}"),
                new Claim(AuthClaimTypes.Username, $"{username}"),
            };
                      
            if (roles != null && roles.Any())
            {
                claims.AddRange(roles.Select(r => new Claim(AuthClaimTypes.roles, r)));
            }

            if (roleaccess != null && roleaccess.Any())
            {
                claims.AddRange(roleaccess.Select(a => new Claim(AuthClaimTypes.RoleAccess, a)));
            }

            var jwt = new JwtSecurityToken(_jwtHeader, new JwtPayload(_issuer, _audience, claims, nowUtc, expires));
            
            return _jwtSecurityTokenHandler.WriteToken(jwt);
        }

        #region private methods

        private void InitJwt(IConfiguration configuration)
        {
            var jwtConfig = configuration.GetSection("JWT");
            _issuer = jwtConfig["Issuer"];
            _audience = jwtConfig["Audience"];

            var _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtConfig["SecretKey"]));
            _signingCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);

            _jwtHeader = new JwtHeader(_signingCredentials);
            Parameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _issuer,
                IssuerSigningKey = _signingKey,
                RequireExpirationTime = true,               
                ClockSkew = TimeSpan.Zero               
            };
        }

        #endregion
    }
}
