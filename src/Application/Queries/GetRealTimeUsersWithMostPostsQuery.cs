using Application.Models;
using MediatR;

namespace Application.Queries;
public class GetRealTimeUsersWithMostPostsQuery : IRequest<List<AuthorPostsCountDetails>>
{
}
