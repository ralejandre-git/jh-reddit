using Application.Abstractions;
using Application.Commands;
using Application.Models;
using Domain.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Queries;

public class GetRealTimeUsersWithMostPostsQueryHandler : IRequestHandler<GetRealTimeUsersWithMostPostsQuery, List<AuthorPostsCountDetails>?>
{
    private readonly ISubRedditPostsRepository _subredditPostsRepository;
    private readonly ILogger<GetRealTimeUsersWithMostPostsQueryHandler> _logger;

    public GetRealTimeUsersWithMostPostsQueryHandler(ISubRedditPostsRepository subRedditPostsRepository,
        ILogger<GetRealTimeUsersWithMostPostsQueryHandler> logger)
    {
        _subredditPostsRepository = subRedditPostsRepository.ThrowIfNull(nameof(subRedditPostsRepository));
        _logger = logger.ThrowIfNull(nameof(logger));
    }

    public Task<List<AuthorPostsCountDetails>?> Handle(GetRealTimeUsersWithMostPostsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogDebug("Calling {Handler}...", nameof(GetRealTimeUsersWithMostPostsQueryHandler));
        return Task.FromResult(_subredditPostsRepository.GetRealTimeUsersWithMostPosts());
    }
}
