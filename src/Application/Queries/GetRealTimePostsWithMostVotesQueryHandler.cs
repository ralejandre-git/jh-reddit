using Application.Abstractions;
using Application.Commands;
using Application.Models;
using Domain.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Queries;

public class GetRealTimePostsWithMostVotesQueryHandler : IRequestHandler<GetRealTimePostsWithMostVotesQuery, List<InsertSubRedditPosts>?>
{
    private readonly ISubRedditPostsRepository _subredditPostsRepository;
    private readonly ILogger<AddPostsWithMostVotesCommandHandler> _logger;

    public GetRealTimePostsWithMostVotesQueryHandler(ISubRedditPostsRepository subRedditPostsRepository,
        ILogger<AddPostsWithMostVotesCommandHandler> logger)
    {
        _subredditPostsRepository = subRedditPostsRepository.ThrowIfNull(nameof(subRedditPostsRepository));
        _logger = logger.ThrowIfNull(nameof(logger));
    }

    public Task<List<InsertSubRedditPosts>?> Handle(GetRealTimePostsWithMostVotesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogDebug("Callng GetRealTimePostsWithMostVotesQueryHandler...");
        return Task.FromResult(_subredditPostsRepository.GetRealTimePostsWithMostVotes());
    }
}
