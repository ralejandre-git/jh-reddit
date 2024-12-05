using Application.Abstractions;
using Application.Models;
using Domain.Abstractions;
using Domain.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;

namespace Application.Commands;

public class AddPostsWithMostVotesCommandHandler : IRequestHandler<AddPostsWithMostVotesCommand, (Result<IEnumerable<InsertSubRedditPosts>>, HttpResponseHeaders)>
{
    private readonly ISubRedditPostsRepository _subredditPostsRepository;
    private readonly ILogger<AddPostsWithMostVotesCommandHandler> _logger;

    public AddPostsWithMostVotesCommandHandler(ISubRedditPostsRepository subRedditPostsRepository,
        ILogger<AddPostsWithMostVotesCommandHandler> logger)
    {
        _subredditPostsRepository = subRedditPostsRepository.ThrowIfNull(nameof(subRedditPostsRepository));
        _logger = logger.ThrowIfNull(nameof(logger));
    }

    public async Task<(Result<IEnumerable<InsertSubRedditPosts>>, HttpResponseHeaders)> Handle(AddPostsWithMostVotesCommand request, CancellationToken cancellationToken)
    {
        //We can use AutoMapper
        var subRedditTop = new SubRedditTop
        {
            SubRedditName = request.SubRedditName,
            SubRedditTimeFrameType = request.SubRedditTimeFrameType.ToString(),
            Limit = request.Limit
        };

        var result = await _subredditPostsRepository.InsertPostsWithMostVotesAsync(subRedditTop);

        return result;
    }
}
