namespace Application.Models;

public class InsertSubRedditPosts
{
    public int BatchId { get; set; }
    public string PostId { get; set; } = default!;
    public string PostName { get; set; } = default!;
    public string PostTitle { get; set; } = default!;
    public int Ups { get; set; } = 0;
    public string Author { get; set; } = default!;
    public string? AuthorFullName { get; set; } = default!;
    public DateTime? PostCreatedUtc { get; set; }
    public string SubRedditName { get; set; } = default!;
    public string SubRedditTimeFrameType { get; set; } = default!;
    public ushort Limit { get; set; }
    public DateTime CreatedDate { get; set; }
}
