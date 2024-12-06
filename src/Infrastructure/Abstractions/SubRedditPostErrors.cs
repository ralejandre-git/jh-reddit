using Domain.Abstractions;
using Infrastructure.Models;

namespace Infrastructure.Abstractions;

public static class SubRedditPostErrors
{
    public static Error UnableToAquireLock() => new ("InsertPostsWithMostVotesAsync.Lock", $"Unable to aquire lock.", (int)ErrorStatusCodeEnum.InternalServerError);

    public static Error NoPostsWithMostVotes() => new ("InsertPostsWithMostVotesAsync.NoPosts", $"There are no posts with most votes in the given subreddit.", (int)ErrorStatusCodeEnum.NotFound);

    public static Error CouldNotPersistPosts() => new("InsertPostsWithMostVotesAsync.CouldNotPersist", $"Could not persist the reddit posts.", (int)ErrorStatusCodeEnum.InternalServerError);
}
