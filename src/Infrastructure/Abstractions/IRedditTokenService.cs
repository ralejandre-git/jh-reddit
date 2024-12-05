using System.IdentityModel.Tokens.Jwt;

namespace Infrastructure.Abstractions;

internal interface IRedditTokenService
{
    Task<JwtSecurityToken> GetRedditTokenAsync();
}
