using Application.Models;
using Domain.Abstractions;
using System.Collections.Concurrent;
using System.Net.Http.Headers;

namespace Application.Abstractions;

public interface ISubRedditPostsRepository
{
    List<InsertSubRedditPosts>? GetRealTimePostsWithMostVotes();
    Task<(Result<IEnumerable<InsertSubRedditPosts>>, HttpResponseHeaders)> InsertPostsWithMostVotesAsync(SubRedditTop subRedditTop);
}
