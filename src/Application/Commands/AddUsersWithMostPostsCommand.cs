using Application.Models;
using Domain.Abstractions;
using MediatR;
using System.Net.Http.Headers;

namespace Application.Commands;

public sealed record AddUsersWithMostPostsCommand(string SubRedditName, 
    string SubRedditTimeFrameType,
    ushort Limit) : IRequest<(Result<IEnumerable<AuthorPostsCountDetails>>, HttpResponseHeaders)>;
