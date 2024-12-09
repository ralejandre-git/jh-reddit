using Domain.Extensions;
using Infrastructure.Abstractions;
using Infrastructure.Models.Requests;
using Infrastructure.Models.Responses;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;

namespace Infrastructure.Services
{
    public class RedditServiceClient : IRedditServiceClient
    {
        private readonly HttpClient _httpClient;
        private readonly IRedditTokenService _redditTokenService;
        private readonly ILogger<RedditServiceClient> _logger;

        //https://oauth.reddit.com/r/biology/top?t=hour&limit=1000&sr_detail=0
        private const string redditTopApiUri = "r/{0}/top?t={1}&limit={2}&sr_detail=0";

        //https://oauth.reddit.com/r/biology/search.json?q=subreddit:biology&restrict_sr=on&t=week&show=all&sort=new&limit=250
        private const string redditSearchApiUri = "r/{0}/search.json?q=subreddit:{1}&restrict_sr=on&t={2}&limit={3}&sort=new";

        public RedditServiceClient(HttpClient httpClient, IRedditTokenService redditTokenService, ILogger<RedditServiceClient> logger)
        {
            _httpClient = httpClient.ThrowIfNull(nameof(httpClient));
            _redditTokenService = redditTokenService.ThrowIfNull(nameof(redditTokenService));
            _logger = logger.ThrowIfNull(nameof(logger));
        }

        public async Task<(RedditListingResponse?, HttpResponseHeaders)> GetPostsWithMostVotesAsync(SubRedditTopRequest subRedditTopRequest)
        {
            var token = await _redditTokenService.GetRedditTokenAsync();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token?.RawData);

            var path = string.Format(redditTopApiUri, subRedditTopRequest.SubRedditName, subRedditTopRequest.SubRedditTimeFrameType, subRedditTopRequest.Limit);
            var redditListingResponse = await _httpClient.GetAsync<RedditListingResponse>(path);

            return redditListingResponse;
        }

        public async Task<(RedditListingResponse?, HttpResponseHeaders)> SearchNewestPostsAsync(SubRedditTopRequest subRedditTopRequest)
        {
            var token = await _redditTokenService.GetRedditTokenAsync();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token?.RawData);

            var path = string.Format(redditSearchApiUri, subRedditTopRequest.SubRedditName, subRedditTopRequest.SubRedditName,
                                    subRedditTopRequest.SubRedditTimeFrameType, subRedditTopRequest.Limit);
            var redditListingResponse = await _httpClient.GetAsync<RedditListingResponse>(path);

            return redditListingResponse;
        }
    }
}
