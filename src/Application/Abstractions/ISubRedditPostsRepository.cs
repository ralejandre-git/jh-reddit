using Application.Models;
using Domain.Abstractions;
using System.Collections.Concurrent;
using System.Net.Http.Headers;

namespace Application.Abstractions;

public interface ISubRedditPostsRepository
{
    List<InsertSubRedditPosts>? GetRealTimePostsWithMostVotes();
    List<AuthorPostsCountDetails>? GetRealTimeUsersWithMostPosts();
    Task<(Result<IEnumerable<InsertSubRedditPosts>>, HttpResponseHeaders)> InsertPostsWithMostVotesAsync(SubRedditTop subRedditTop);

    Task<(Result<IEnumerable<AuthorPostsCountDetails>>, HttpResponseHeaders)> InsertUsersWithMostPostsAsync(SubRedditTop subRedditTop);
}
