using Application.Commands;
using System.Collections;

namespace Reddit.Tests.Unit.Common;

public class AddPostsWithMostVotesCommandTestData : IEnumerable<object[]>
{
    //Provides data for all negative cases that should throw validation error
    public IEnumerator<object[]> GetEnumerator()
    {
        var addPostsWithMostVotesCommandWithProp = new object[] { SubRedditMockData.GetAddPostsWithMostVotesCommand("", "hour"), nameof(AddPostsWithMostVotesCommand.SubRedditName) };
        yield return addPostsWithMostVotesCommandWithProp;

        addPostsWithMostVotesCommandWithProp = [SubRedditMockData.GetAddPostsWithMostVotesCommand(null, "hour"), nameof(AddPostsWithMostVotesCommand.SubRedditName)];
        yield return addPostsWithMostVotesCommandWithProp;

        addPostsWithMostVotesCommandWithProp = [SubRedditMockData.GetAddPostsWithMostVotesCommand(" ", "hour"), nameof(AddPostsWithMostVotesCommand.SubRedditName)];
        yield return addPostsWithMostVotesCommandWithProp;


        addPostsWithMostVotesCommandWithProp = [SubRedditMockData.GetAddPostsWithMostVotesCommand("biology", ""), nameof(AddPostsWithMostVotesCommand.SubRedditTimeFrameType)];
        yield return addPostsWithMostVotesCommandWithProp;

        addPostsWithMostVotesCommandWithProp = [SubRedditMockData.GetAddPostsWithMostVotesCommand("biology", null), nameof(AddPostsWithMostVotesCommand.SubRedditTimeFrameType)];
        yield return addPostsWithMostVotesCommandWithProp;

        addPostsWithMostVotesCommandWithProp = [SubRedditMockData.GetAddPostsWithMostVotesCommand("biology", " "), nameof(AddPostsWithMostVotesCommand.SubRedditTimeFrameType)];
        yield return addPostsWithMostVotesCommandWithProp;

        addPostsWithMostVotesCommandWithProp = [SubRedditMockData.GetAddPostsWithMostVotesCommand("biology", "quarter"), nameof(AddPostsWithMostVotesCommand.SubRedditTimeFrameType)];
        yield return addPostsWithMostVotesCommandWithProp;

        var negativeValue = -1;
        addPostsWithMostVotesCommandWithProp = [SubRedditMockData.GetAddPostsWithMostVotesCommand("biology", "hour", (ushort)negativeValue), nameof(AddPostsWithMostVotesCommand.Limit)];
        yield return addPostsWithMostVotesCommandWithProp;

        addPostsWithMostVotesCommandWithProp = [SubRedditMockData.GetAddPostsWithMostVotesCommand("biology", "hour", 251), nameof(AddPostsWithMostVotesCommand.Limit)];
        yield return addPostsWithMostVotesCommandWithProp;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
