using System.IdentityModel.Tokens.Jwt;

namespace Infrastructure.Abstractions;

public interface IRedditTokenService
{
    Task<JwtSecurityToken> GetRedditTokenAsync();
}
