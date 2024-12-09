using Application.Abstractions;
using Application.Models;
using Domain.Abstractions;
using Domain.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;

namespace Application.Commands;

public class AddUsersWithMostPostsCommandHandler : IRequestHandler<AddUsersWithMostPostsCommand, (Result<IEnumerable<AuthorPostsCountDetails>>, HttpResponseHeaders)>
{
    private readonly ISubRedditPostsRepository _subredditPostsRepository;
    private readonly ILogger<AddUsersWithMostPostsCommandHandler> _logger;

    public AddUsersWithMostPostsCommandHandler(ISubRedditPostsRepository subRedditPostsRepository,
        ILogger<AddUsersWithMostPostsCommandHandler> logger)
    {
        _subredditPostsRepository = subRedditPostsRepository.ThrowIfNull(nameof(subRedditPostsRepository));
        _logger = logger.ThrowIfNull(nameof(logger));
    }

    public async Task<(Result<IEnumerable<AuthorPostsCountDetails>>, HttpResponseHeaders)> Handle(AddUsersWithMostPostsCommand request, CancellationToken cancellationToken)
    {
        //We can use AutoMapper
        var subRedditTop = new SubRedditTop
        {
            SubRedditName = request.SubRedditName,
            SubRedditTimeFrameType = request.SubRedditTimeFrameType.ToString(),
            Limit = request.Limit
        };

        var result = await _subredditPostsRepository.InsertUsersWithMostPostsAsync(subRedditTop);

        return result;
    }
}