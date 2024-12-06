using Application.Models;
using Domain.Abstractions;
using Infrastructure.Abstractions;
using Infrastructure.Models;
using Infrastructure.Models.Requests;
using Infrastructure.Models.Responses;
using Infrastructure.Repositories;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Reddit.Tests.Unit.Common;

namespace Reddit.Tests.Unit.Repository;

public class SubRedditPostsRepositoryUnitTests
{
    private readonly SubRedditPostsRepository _subRedditPostsRepository;
    private readonly IRedditService _redditService;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ILogger<SubRedditPostsRepository> _logger;

    public SubRedditPostsRepositoryUnitTests()
    {
        _redditService = Substitute.For<IRedditService>();
        _dateTimeProvider = Substitute.For<IDateTimeProvider>();
        _logger = Substitute.For<ILogger<SubRedditPostsRepository>>();

        _subRedditPostsRepository = new SubRedditPostsRepository(_redditService, _dateTimeProvider, _logger);
    }

    [Fact]
    public async Task InsertPostsWithMostVotesAsync_Should_Add_Top_Posts_To_Store()
    {
        //Arrange
        var subRedditTop = SubRedditMockData.GetSubRedditTop("biology", "hour", 1);

        _redditService.GetPostsWithMostVotesAsync(Arg.Any<SubRedditTopRequest>()).Returns(SubRedditMockData.GetRedditListingResponseAndHeadersTuple());

        //Act
        var result = await _subRedditPostsRepository.InsertPostsWithMostVotesAsync(subRedditTop);

        //Assert
        Assert.True(result.Item1.IsSuccess);
        Assert.NotEmpty(result.Item1?.Value!);
        var insertSubRedditPosts = result.Item1?.Value?.ToList();
        Assert.True(SubRedditPostsRepository.PostsWithMostVotesStore.TryGetValue(1, out var subRedditPostsFromStore));
        Assert.Equal(insertSubRedditPosts?.Count, subRedditPostsFromStore.Count);
        Assert.Equal(insertSubRedditPosts?.FirstOrDefault()?.PostId, subRedditPostsFromStore.FirstOrDefault()?.PostId);

        await _redditService.Received().GetPostsWithMostVotesAsync(Arg.Any<SubRedditTopRequest>());
    }

    [Fact]
    public async Task InsertPostsWithMostVotesAsync_Should_Fail_When_No_Posts_Found_To_Add_To_Store()
    {
        //Arrange
        var subRedditTop = SubRedditMockData.GetSubRedditTop("biology", "hour", 1);

        _redditService.GetPostsWithMostVotesAsync(Arg.Any<SubRedditTopRequest>()).Returns((null, default!));

        //Act
        var result = await _subRedditPostsRepository.InsertPostsWithMostVotesAsync(subRedditTop);

        //Assert
        Assert.False(result.Item1.IsSuccess);
        Assert.NotNull(result.Item1?.Error);
        Assert.Equal((int)ErrorStatusCodeEnum.NotFound, result.Item1?.Error.StatusCode);
        //TODO: Add hard-coded codes as constants
        Assert.Equal("InsertPostsWithMostVotesAsync.NoPosts", result.Item1?.Error.Code);
        Assert.False(SubRedditPostsRepository.PostsWithMostVotesStore.TryGetValue(1, out _));

        await _redditService.Received().GetPostsWithMostVotesAsync(Arg.Any<SubRedditTopRequest>());
    }
}
