using Application.Abstractions;
using Application.Models;
using Domain.Abstractions;
using Domain.Extensions;
using Infrastructure.Abstractions;
using Infrastructure.Models.Requests;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading.Tasks.Dataflow;

namespace Infrastructure.Repositories;

public class SubRedditPostsRepository : ISubRedditPostsRepository
{
    private readonly IRedditServiceClient _redditService;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ILogger<SubRedditPostsRepository> _logger;

    public static ConcurrentDictionary<int, List<InsertSubRedditPosts>> PostsWithMostVotesStore { get; private set; } = [];

    public SubRedditPostsRepository(IRedditServiceClient redditService,
        IDateTimeProvider dateTimeProvider,
        ILogger<SubRedditPostsRepository> logger)
    {
        _redditService = redditService.ThrowIfNull(nameof(redditService));
        _dateTimeProvider = dateTimeProvider.ThrowIfNull(nameof(dateTimeProvider));
        _logger = logger.ThrowIfNull(nameof(logger));
    }

    public List<InsertSubRedditPosts>? GetRealTimePostsWithMostVotes()
    {
        if (PostsWithMostVotesStore.TryGetNonEnumeratedCount(out int key))
        {
            var subRedditPosts = PostsWithMostVotesStore.GetValueOrDefault(key);

            if (subRedditPosts != null)
            {
                subRedditPosts.ForEach(post => post.BatchId = key);
                return subRedditPosts;
            }
        }

        return default!;
    }

    /// <summary>
    /// Get posts with most votes from Reddit. Then persist them to a Dictionary.
    /// It returns the Key
    /// </summary>
    /// <param name="subRedditTop"></param>
    /// <returns></returns>
    /// <exception cref="TimeoutException"></exception>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<(Result<IEnumerable<InsertSubRedditPosts>>, HttpResponseHeaders)> InsertPostsWithMostVotesAsync(SubRedditTop subRedditTop)
    {
        //We can use AutoMapper
        var subRedditTopRequest = new SubRedditTopRequest(subRedditTop.SubRedditName, subRedditTop.SubRedditTimeFrameType, subRedditTop.Limit);

        (var postsWithMostVotesResponse, var responseHeaders) = await _redditService.GetPostsWithMostVotesAsync(subRedditTopRequest);

        if (postsWithMostVotesResponse != null && postsWithMostVotesResponse.Data != null &&
            postsWithMostVotesResponse.Data.Children?.Count > 0)
        {
            var postsWithMostVotes = postsWithMostVotesResponse.Data.Children.Where(post => post.Data != null);

            var subRedditPostsBulk = postsWithMostVotes.Select(post => new InsertSubRedditPosts {
                PostId = post.Data!.Id ?? string.Empty,
                PostName = post.Data!.Name ?? string.Empty,
                PostTitle = post.Data!.Title ?? string.Empty,
                Ups = post.Data!.Ups ?? 0,
                Author = post.Data!.Author ?? string.Empty,
                PostCreatedUtc = DateTime.UnixEpoch.AddSeconds(post.Data!.CreatedUtc ?? 0.0),
                SubRedditName = post.Data!.Subreddit ?? string.Empty,
                SubRedditTimeFrameType = subRedditTop.SubRedditTimeFrameType,
                Limit = subRedditTop.Limit,
                CreatedDate = _dateTimeProvider.GetCurrentTime()
            });

            var semaphoreSlim = new SemaphoreSlim(1);
            var aquired = await semaphoreSlim.WaitAsync(300); // this timeout needs to be externalized.

            if (!aquired)
            {
                _logger.LogError("@{Method} Unable to acquire lock", nameof(SubRedditPostsRepository));

                return (Result<IEnumerable<InsertSubRedditPosts>>.Failure(SubRedditPostErrors.UnableToAquireLock()), responseHeaders);
            }                

            try
            {
                if (PostsWithMostVotesStore.TryGetNonEnumeratedCount(out int key)) 
                {
                    var couldAddPosts = PostsWithMostVotesStore.TryAdd(++key, subRedditPostsBulk.ToList());

                    if (!couldAddPosts)
                    {
                        return (Result<IEnumerable<InsertSubRedditPosts>>.Failure(SubRedditPostErrors.CouldNotPersistPosts()), responseHeaders);
                    }

                    _logger.LogDebug("Posts with most votes were persisted, with batch Id: @{key}", key);
                    return (Result<IEnumerable<InsertSubRedditPosts>>.Success(subRedditPostsBulk), responseHeaders);
                }
            }
            finally
            {
                semaphoreSlim.Release();
            }
        }

        return (Result<IEnumerable<InsertSubRedditPosts>>.Failure(SubRedditPostErrors.NoPostsWithMostVotes()), responseHeaders);
    }
}
