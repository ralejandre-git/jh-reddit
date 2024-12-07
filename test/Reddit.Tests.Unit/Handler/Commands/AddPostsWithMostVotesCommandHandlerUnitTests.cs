using Application.Abstractions;
using Application.Behaviors;
using Application.Commands;
using Application.Models;
using Domain.Abstractions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Reddit.Tests.Unit.Common;
using System.Net.Http.Headers;

namespace Reddit.Tests.Unit.Handler.Commands;

public class AddPostsWithMostVotesCommandHandlerUnitTests
{
    private readonly AddPostsWithMostVotesCommandHandler _handler;
    private readonly ISubRedditPostsRepository _subRedditPostsRepository;
    private readonly ILogger<AddPostsWithMostVotesCommandHandler> _logger;

    public AddPostsWithMostVotesCommandHandlerUnitTests()
    {
        _subRedditPostsRepository = Substitute.For<ISubRedditPostsRepository>();
        _logger = Substitute.For<ILogger<AddPostsWithMostVotesCommandHandler>>();

        _handler = new(_subRedditPostsRepository, _logger);
    }

    [Fact]
    public async Task AddPostsWithMostVotesCommand_Should_Succeed_Validation_And_Add_Posts_And_Return_Result()
    {
        // Arrange
        var command = SubRedditMockData.GetAddPostsWithMostVotesCommand("biology", "hour", 1);
        var insertSubredditPostsMockData = SubRedditMockData.GetInsertSubredditPostsList();

        var validationBehavior = new ValidationBehavior<AddPostsWithMostVotesCommand, (Result<IEnumerable<InsertSubRedditPosts>>, HttpResponseHeaders)>
            (new List<AddPostsWithMostVotesCommandValidator>() { new () });

        _subRedditPostsRepository.InsertPostsWithMostVotesAsync(Arg.Any<SubRedditTop>())
            .Returns((insertSubredditPostsMockData, default!));

        // Act
        var result = await validationBehavior.Handle(command, async () =>
        {
            return await _handler.Handle(command, CancellationToken.None);
        }, CancellationToken.None);

        //Assert
        Assert.True(result.Item1.IsSuccess);
        Assert.NotEmpty(result.Item1?.Value!);

        var insertSubRedditPosts = result.Item1?.Value?.ToList();
        Assert.Equal(insertSubredditPostsMockData?.Count, insertSubRedditPosts?.Count);
        Assert.Equal(insertSubRedditPosts?.FirstOrDefault()?.PostId, insertSubRedditPosts?.FirstOrDefault()?.PostId);
        await _subRedditPostsRepository.Received().InsertPostsWithMostVotesAsync(Arg.Any<SubRedditTop>());
    }

    [Fact]
    public async Task AddPostsWithMostVotesCommand_Fails_Validation_On_Invalid_Command_And_Throws_Exception()
    {
        //Arrange
        var command = SubRedditMockData.GetAddPostsWithMostVotesCommand("", "hour", 1);
        var insertSubredditPostsMockData = SubRedditMockData.GetInsertSubredditPostsList();

        var validationBehavior = new ValidationBehavior<AddPostsWithMostVotesCommand, (Result<IEnumerable<InsertSubRedditPosts>>, HttpResponseHeaders)>
            (new List<AddPostsWithMostVotesCommandValidator>() { new() });

        //Act & Assert
        await Assert.ThrowsAnyAsync<ValidationException>(() =>
            validationBehavior.Handle(command, async () =>
            {
                return await _handler.Handle(command, CancellationToken.None);
            }, CancellationToken.None)
        );

        await _subRedditPostsRepository.DidNotReceive().InsertPostsWithMostVotesAsync(Arg.Any<SubRedditTop>());
    }
}
