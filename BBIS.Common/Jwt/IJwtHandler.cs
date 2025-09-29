using Microsoft.IdentityModel.Tokens;

namespace BBIS.Common.Jwt
{
    public interface IJwtHandler
    {
        string Create(Guid userId, string username, string fullName, List<string> roles, List<string> roleaccess);
        TokenValidationParameters Parameters { get; }
    }
}
