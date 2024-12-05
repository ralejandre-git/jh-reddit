using Newtonsoft.Json;

namespace Infrastructure.Models.Responses;

public class RedditListingResponse
{
    [JsonProperty("kind")]
    public string? Kind { get; set; }

    [JsonProperty("data")]
    public Data? Data { get; set; }
}

public class Child
{
    [JsonProperty("kind")]
    public string? Kind { get; set; }

    [JsonProperty("data")]
    public Data? Data { get; set; }
}

public class Data
{
    [JsonProperty("after")]
    public string? After { get; set; }

    [JsonProperty("dist")]
    public int? Dist { get; set; }

    [JsonProperty("modhash")]
    public string? Modhash { get; set; }

    [JsonProperty("geo_filter")]
    public string? GeoFilter { get; set; }

    [JsonProperty("children")]
    public List<Child>? Children { get; set; }

    [JsonProperty("before")]
    public string? Before { get; set; }

    [JsonProperty("approved_at_utc")]
    public DateTime? ApprovedAtUtc { get; set; }

    [JsonProperty("subreddit")]
    public string? Subreddit { get; set; }

    [JsonProperty("selftext")]
    public string? Selftext { get; set; }

    [JsonProperty("author_fullname")]
    public string? AuthorFullname { get; set; }

    [JsonProperty("saved")]
    public bool? Saved { get; set; }

    [JsonProperty("mod_reason_title")]
    public string? ModReasonTitle { get; set; }

    [JsonProperty("gilded")]
    public int? Gilded { get; set; }

    [JsonProperty("clicked")]
    public bool? Clicked { get; set; }

    [JsonProperty("title")]
    public string? Title { get; set; }

    [JsonProperty("link_flair_richtext")]
    public List<LinkFlairRichtext>? LinkFlairRichtext { get; set; }

    [JsonProperty("subreddit_name_prefixed")]
    public string? SubredditNamePrefixed { get; set; }

    [JsonProperty("hidden")]
    public bool? Hidden { get; set; }

    [JsonProperty("pwls")]
    public int? Pwls { get; set; }

    [JsonProperty("link_flair_css_class")]
    public string? LinkFlairCssClass { get; set; }

    [JsonProperty("downs")]
    public int? Downs { get; set; }

    [JsonProperty("top_awarded_type")]
    public string? TopAwardedType { get; set; }

    [JsonProperty("hide_score")]
    public bool? HideScore { get; set; }

    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("quarantine")]
    public bool? Quarantine { get; set; }

    [JsonProperty("link_flair_text_color")]
    public string? LinkFlairTextColor { get; set; }

    [JsonProperty("upvote_ratio")]
    public double? UpvoteRatio { get; set; }

    [JsonProperty("author_flair_background_color")]
    public string? AuthorFlairBackgroundColor { get; set; }

    [JsonProperty("subreddit_type")]
    public string? SubredditType { get; set; }

    [JsonProperty("ups")]
    public int? Ups { get; set; }

    [JsonProperty("total_awards_received")]
    public int? TotalAwardsReceived { get; set; }

    [JsonProperty("media_embed")]
    public MediaEmbed? MediaEmbed { get; set; }

    [JsonProperty("author_flair_template_id")]
    public string? AuthorFlairTemplateId { get; set; }

    [JsonProperty("is_original_content")]
    public bool? IsOriginalContent { get; set; }

    [JsonProperty("user_reports")]
    public List<object>? UserReports { get; set; }

    [JsonProperty("secure_media")]
    public object? SecureMedia { get; set; }

    [JsonProperty("is_reddit_media_domain")]
    public bool? IsRedditMediaDomain { get; set; }

    [JsonProperty("is_meta")]
    public bool? IsMeta { get; set; }

    [JsonProperty("category")]
    public string? Category { get; set; }

    [JsonProperty("secure_media_embed")]
    public SecureMediaEmbed? SecureMediaEmbed { get; set; }

    [JsonProperty("link_flair_text")]
    public string? LinkFlairText { get; set; }

    [JsonProperty("can_mod_post")]
    public bool? CanModPost { get; set; }

    [JsonProperty("score")]
    public int? Score { get; set; }

    [JsonProperty("approved_by")]
    public string? ApprovedBy { get; set; }

    [JsonProperty("is_created_from_ads_ui")]
    public bool? IsCreatedFromAdsUi { get; set; }

    [JsonProperty("author_premium")]
    public bool? AuthorPremium { get; set; }

    [JsonProperty("thumbnail")]
    public string? Thumbnail { get; set; }

    [JsonProperty("edited")]
    public bool? Edited { get; set; }

    [JsonProperty("author_flair_css_class")]
    public string? AuthorFlairCssClass { get; set; }

    [JsonProperty("author_flair_richtext")]
    public List<LinkFlairRichtext>? AuthorFlairRichtext { get; set; }

    [JsonProperty("gildings")]
    public Gildings? Gildings { get; set; }

    [JsonProperty("content_categories")]
    public string? ContentCategories { get; set; }

    [JsonProperty("is_self")]
    public bool? IsSelf { get; set; }

    [JsonProperty("mod_note")]
    public string? ModNote { get; set; }

    [JsonProperty("created")]
    public double? Created { get; set; }

    [JsonProperty("link_flair_type")]
    public string? LinkFlairType { get; set; }

    [JsonProperty("wls")]
    public int? Wls { get; set; }

    [JsonProperty("removed_by_category")]
    public string? RemovedByCategory { get; set; }

    [JsonProperty("banned_by")]
    public string? BannedBy { get; set; }

    [JsonProperty("author_flair_type")]
    public string? AuthorFlairType { get; set; }

    [JsonProperty("domain")]
    public string? Domain { get; set; }

    [JsonProperty("allow_live_comments")]
    public bool? AllowLiveComments { get; set; }

    [JsonProperty("selftext_html")]
    public string? SelftextHtml { get; set; }

    [JsonProperty("likes")]
    public string? Likes { get; set; }

    [JsonProperty("suggested_sort")]
    public string? SuggestedSort { get; set; }

    [JsonProperty("banned_at_utc")]
    public string? BannedAtUtc { get; set; }

    [JsonProperty("view_count")]
    public int? ViewCount { get; set; }

    [JsonProperty("archived")]
    public bool? Archived { get; set; }

    [JsonProperty("no_follow")]
    public bool? NoFollow { get; set; }

    [JsonProperty("is_crosspostable")]
    public bool? IsCrosspostable { get; set; }

    [JsonProperty("pinned")]
    public bool? Pinned { get; set; }

    [JsonProperty("over_18")]
    public bool? Over18 { get; set; }

    [JsonProperty("all_awardings")]
    public List<object>? AllAwardings { get; set; }

    [JsonProperty("awarders")]
    public List<object>? Awarders { get; set; }

    [JsonProperty("media_only")]
    public bool? MediaOnly { get; set; }

    [JsonProperty("link_flair_template_id")]
    public string? LinkFlairTemplateId { get; set; }

    [JsonProperty("can_gild")]
    public bool? CanGild { get; set; }

    [JsonProperty("spoiler")]
    public bool? Spoiler { get; set; }

    [JsonProperty("locked")]
    public bool? Locked { get; set; }

    [JsonProperty("author_flair_text")]
    public string? AuthorFlairText { get; set; }

    [JsonProperty("treatment_tags")]
    public List<string>? TreatmentTags { get; set; }

    [JsonProperty("visited")]
    public bool? Visited { get; set; }

    [JsonProperty("removed_by")]
    public string? RemovedBy { get; set; }

    [JsonProperty("num_reports")]
    public string? NumReports { get; set; }

    [JsonProperty("distinguished")]
    public string? Distinguished { get; set; }

    [JsonProperty("subreddit_id")]
    public string? SubredditId { get; set; }

    [JsonProperty("author_is_blocked")]
    public bool? AuthorIsBlocked { get; set; }

    [JsonProperty("mod_reason_by")]
    public string? ModReasonBy { get; set; }

    [JsonProperty("removal_reason")]
    public string? RemovalReason { get; set; }

    [JsonProperty("link_flair_background_color")]
    public string? LinkFlairBackgroundColor { get; set; }

    [JsonProperty("id")]
    public string? Id { get; set; }

    [JsonProperty("is_robot_indexable")]
    public bool? IsRobotIndexable { get; set; }

    [JsonProperty("report_reasons")]
    public string? ReportReasons { get; set; }

    [JsonProperty("author")]
    public string? Author { get; set; }

    [JsonProperty("discussion_type")]
    public string? DiscussionType { get; set; }

    [JsonProperty("num_comments")]
    public int? NumComments { get; set; }

    [JsonProperty("send_replies")]
    public bool? SendReplies { get; set; }

    [JsonProperty("contest_mode")]
    public bool? ContestMode { get; set; }

    [JsonProperty("mod_reports")]
    public List<object>? ModReports { get; set; }

    [JsonProperty("author_patreon_flair")]
    public bool? AuthorPatreonFlair { get; set; }

    [JsonProperty("author_flair_text_color")]
    public string? AuthorFlairTextColor { get; set; }

    [JsonProperty("permalink")]
    public string? Permalink { get; set; }

    [JsonProperty("stickied")]
    public bool? Stickied { get; set; }

    [JsonProperty("url")]
    public string? Url { get; set; }

    [JsonProperty("subreddit_subscribers")]
    public int? SubredditSubscribers { get; set; }

    [JsonProperty("created_utc")]
    public double? CreatedUtc { get; set; }

    [JsonProperty("num_crossposts")]
    public int? NumCrossposts { get; set; }

    [JsonProperty("media")]
    public object? Media { get; set; }

    [JsonProperty("is_video")]
    public bool? IsVideo { get; set; }
}

public class Gildings
{
}

public class LinkFlairRichtext
{
    [JsonProperty("a")]
    public string A { get; set; }

    [JsonProperty("e")]
    public string E { get; set; }

    [JsonProperty("u")]
    public string U { get; set; }

    [JsonProperty("t")]
    public string T { get; set; }
}

public class MediaEmbed
{
}
public class SecureMediaEmbed
{
}