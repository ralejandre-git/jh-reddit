namespace Infrastructure.Models.Requests;

/// <summary>
/// Sample request: https://oauth.reddit.com/r/biology/top?t=hour&limit=100&sr_detail=0
/// </summary>
public record SubRedditTopRequest(string SubRedditName, string SubRedditTimeFrameType, ushort Limit);
