using Domain.Abstractions;
using Infrastructure.Models;

namespace Infrastructure.Abstractions;

public static class UsersWithMostPostsErrors
{
    public static Error UnableToAquireLock() => new("InsertUsersWithMostPostsAsync.Lock", $"Unable to aquire lock.", (int)ErrorStatusCodeEnum.InternalServerError);

    public static Error NoUsersWithMostPosts() => new("InsertUsersWithMostPostsAsync.NoPosts", $"There are no users with most posts in the given subreddit.", (int)ErrorStatusCodeEnum.NotFound);

    public static Error CouldNotPersistPosts() => new("InsertUsersWithMostPostsAsync.CouldNotPersist", $"Could not persist the reddit posts.", (int)ErrorStatusCodeEnum.InternalServerError);
}
