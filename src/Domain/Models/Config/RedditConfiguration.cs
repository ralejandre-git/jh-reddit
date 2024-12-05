
namespace Domain.Models.Config;

public class RedditConfiguration
{
    public static string Section = "RedditConfiguration";

    public int MaxQueriesPerMinute { get; set; } = 100;
    public RedditAuth? RedditAuth { get; set; }

    public string TimeFrameType { get; set; } = default!;
    public ushort Limit { get; set; }
    public List<string>? SubRedditList { get; set; }
}
