using Infrastructure.Models.Requests;
using Infrastructure.Models.Responses;
using System.Net.Http.Headers;

namespace Infrastructure.Abstractions;

public interface IRedditService
{
    Task<(RedditListingResponse?, HttpResponseHeaders)> GetPostsWithMostVotesAsync(SubRedditTopRequest subRedditTopRequest);
    Task<RedditListingResponse?> GetUseresWithMostPostsAsync();
}
