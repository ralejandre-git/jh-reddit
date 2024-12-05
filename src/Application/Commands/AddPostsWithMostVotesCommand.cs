using Application.Models;
using Domain.Abstractions;
using MediatR;
using System.Net.Http.Headers;

namespace Application.Commands;

public sealed record AddPostsWithMostVotesCommand(string SubRedditName, 
    string SubRedditTimeFrameType,
    ushort Limit) : IRequest<(Result<IEnumerable<InsertSubRedditPosts>>, HttpResponseHeaders)>;
