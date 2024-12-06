using Application.Models;
using MediatR;

namespace Application.Queries;
public class GetRealTimePostsWithMostVotesQuery : IRequest<List<InsertSubRedditPosts>>
{
}
