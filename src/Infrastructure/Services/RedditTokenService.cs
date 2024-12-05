
using Domain.Extensions;
using Infrastructure.Abstractions;
using Infrastructure.Models.Requests;
using Infrastructure.Models.Responses;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;

namespace Infrastructure.Services;

internal class RedditTokenService : IRedditTokenService
{
    private readonly HttpClient _httpClient;
    private readonly JwtSecurityTokenHandler _jwtHandler = new();

    private const string accessTokenUri = "api/v1/access_token";

    public RedditTokenService(HttpClient httpClient, ILogger<RedditTokenService> logger)
    {
        _httpClient = httpClient.ThrowIfNull(nameof(httpClient));
    }

    public async Task<JwtSecurityToken> GetRedditTokenAsync()
    {
        var form = new Dictionary<string, string>
        {
            { "grant_type", new RedditTokenRequest().grant_type}
        };

        var content = new FormUrlEncodedContent(form);

        var response = await _httpClient.PostAsync<RedditTokenResponse>(accessTokenUri, content);

        var jwtToken = _jwtHandler.ReadJwtToken(response?.AccessToken);

        return jwtToken;
    }
}
