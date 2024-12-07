using Application.Commands;
using Application.Models;
using Domain.Abstractions;
using Infrastructure.Models.Requests;
using Infrastructure.Models.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Reddit.Tests.Unit.Common
{
    internal class SubRedditMockData
    {
        public static SubRedditTop GetSubRedditTop(string subRedditName, string subRedditTimeFrameType, ushort limit)
        {
            return new SubRedditTop
            {
                SubRedditName = subRedditName,
                SubRedditTimeFrameType = subRedditTimeFrameType,
                Limit = limit
            };
        }

        public static (RedditListingResponse?, HttpResponseHeaders) GetRedditListingResponseAndHeadersTuple()
        {
            var redditListingResponseJson = "{\"kind\":\"Listing\",\"data\":{\"after\":\"t3_1h7u051\",\"dist\":1,\"modhash\":\"\",\"geo_filter\":\"\",\"children\":[{\"kind\":\"t3\",\"data\":{\"after\":null,\"dist\":null,\"modhash\":null,\"geo_filter\":null,\"children\":null,\"before\":null,\"approved_at_utc\":null,\"subreddit\":\"biology\",\"selftext\":\"Exactly what's on the tin. Some friends and I are having an argument about it currently, but none of us are biologists. So we decided to turn to reddit for some help.\",\"author_fullname\":\"t2_q77837sg\",\"saved\":false,\"mod_reason_title\":null,\"gilded\":0,\"clicked\":false,\"title\":\"Are humans considered raw when alive or is there a distinction between meat that is connected to a sentient organism versus meat from an organism that is deceased?\",\"link_flair_richtext\":[{\"a\":\":snoo_thoughtful:\",\"e\":\"emoji\",\"u\":\"https://emoji.redditmedia.com/7jhvwsc5aezz_t5_3nqvj/snoo_thoughtful\",\"t\":null},{\"a\":null,\"e\":\"text\",\"u\":null,\"t\":\" question\"}],\"subreddit_name_prefixed\":\"r/biology\",\"hidden\":false,\"pwls\":6,\"link_flair_css_class\":\"\",\"downs\":0,\"top_awarded_type\":null,\"hide_score\":true,\"name\":\"t3_1h7u051\",\"quarantine\":false,\"link_flair_text_color\":\"light\",\"upvote_ratio\":0.2,\"author_flair_background_color\":null,\"subreddit_type\":\"public\",\"ups\":0,\"total_awards_received\":0,\"media_embed\":{},\"author_flair_template_id\":null,\"is_original_content\":false,\"user_reports\":[],\"secure_media\":null,\"is_reddit_media_domain\":false,\"is_meta\":false,\"category\":null,\"secure_media_embed\":{},\"link_flair_text\":\":snoo_thoughtful: question\",\"can_mod_post\":false,\"score\":0,\"approved_by\":null,\"is_created_from_ads_ui\":false,\"author_premium\":false,\"thumbnail\":\"\",\"edited\":false,\"author_flair_css_class\":null,\"author_flair_richtext\":[],\"gildings\":{},\"content_categories\":null,\"is_self\":true,\"mod_note\":null,\"created\":1733462252.0,\"link_flair_type\":\"richtext\",\"wls\":6,\"removed_by_category\":null,\"banned_by\":null,\"author_flair_type\":\"text\",\"domain\":\"self.biology\",\"allow_live_comments\":false,\"selftext_html\":\"&lt;!-- SC_OFF --&gt;&lt;div class=\\\"md\\\"&gt;&lt;p&gt;Exactly what&amp;#39;s on the tin. Some friends and I are having an argument about it currently, but none of us are biologists. So we decided to turn to reddit for some help.&lt;/p&gt;\\n&lt;/div&gt;&lt;!-- SC_ON --&gt;\",\"likes\":null,\"suggested_sort\":null,\"banned_at_utc\":null,\"view_count\":null,\"archived\":false,\"no_follow\":true,\"is_crosspostable\":false,\"pinned\":false,\"over_18\":false,\"all_awardings\":[],\"awarders\":[],\"media_only\":false,\"link_flair_template_id\":\"9312ad9e-07ae-11e3-a456-22000ab3216d\",\"can_gild\":false,\"spoiler\":false,\"locked\":false,\"author_flair_text\":null,\"treatment_tags\":[],\"visited\":false,\"removed_by\":null,\"num_reports\":null,\"distinguished\":null,\"subreddit_id\":\"t5_2qhn7\",\"author_is_blocked\":false,\"mod_reason_by\":null,\"removal_reason\":null,\"link_flair_background_color\":\"#349e48\",\"id\":\"1h7u051\",\"is_robot_indexable\":true,\"report_reasons\":null,\"author\":\"LotusPeachShadows\",\"discussion_type\":null,\"num_comments\":9,\"send_replies\":true,\"contest_mode\":false,\"mod_reports\":[],\"author_patreon_flair\":false,\"author_flair_text_color\":null,\"permalink\":\"/r/biology/comments/1h7u051/are_humans_considered_raw_when_alive_or_is_there/\",\"stickied\":false,\"url\":\"https://www.reddit.com/r/biology/comments/1h7u051/are_humans_considered_raw_when_alive_or_is_there/\",\"subreddit_subscribers\":4930887,\"created_utc\":1733462252.0,\"num_crossposts\":0,\"media\":null,\"is_video\":false}}],\"before\":null,\"approved_at_utc\":null,\"subreddit\":null,\"selftext\":null,\"author_fullname\":null,\"saved\":null,\"mod_reason_title\":null,\"gilded\":null,\"clicked\":null,\"title\":null,\"link_flair_richtext\":null,\"subreddit_name_prefixed\":null,\"hidden\":null,\"pwls\":null,\"link_flair_css_class\":null,\"downs\":null,\"top_awarded_type\":null,\"hide_score\":null,\"name\":null,\"quarantine\":null,\"link_flair_text_color\":null,\"upvote_ratio\":null,\"author_flair_background_color\":null,\"subreddit_type\":null,\"ups\":null,\"total_awards_received\":null,\"media_embed\":null,\"author_flair_template_id\":null,\"is_original_content\":null,\"user_reports\":null,\"secure_media\":null,\"is_reddit_media_domain\":null,\"is_meta\":null,\"category\":null,\"secure_media_embed\":null,\"link_flair_text\":null,\"can_mod_post\":null,\"score\":null,\"approved_by\":null,\"is_created_from_ads_ui\":null,\"author_premium\":null,\"thumbnail\":null,\"edited\":null,\"author_flair_css_class\":null,\"author_flair_richtext\":null,\"gildings\":null,\"content_categories\":null,\"is_self\":null,\"mod_note\":null,\"created\":null,\"link_flair_type\":null,\"wls\":null,\"removed_by_category\":null,\"banned_by\":null,\"author_flair_type\":null,\"domain\":null,\"allow_live_comments\":null,\"selftext_html\":null,\"likes\":null,\"suggested_sort\":null,\"banned_at_utc\":null,\"view_count\":null,\"archived\":null,\"no_follow\":null,\"is_crosspostable\":null,\"pinned\":null,\"over_18\":null,\"all_awardings\":null,\"awarders\":null,\"media_only\":null,\"link_flair_template_id\":null,\"can_gild\":null,\"spoiler\":null,\"locked\":null,\"author_flair_text\":null,\"treatment_tags\":null,\"visited\":null,\"removed_by\":null,\"num_reports\":null,\"distinguished\":null,\"subreddit_id\":null,\"author_is_blocked\":null,\"mod_reason_by\":null,\"removal_reason\":null,\"link_flair_background_color\":null,\"id\":null,\"is_robot_indexable\":null,\"report_reasons\":null,\"author\":null,\"discussion_type\":null,\"num_comments\":null,\"send_replies\":null,\"contest_mode\":null,\"mod_reports\":null,\"author_patreon_flair\":null,\"author_flair_text_color\":null,\"permalink\":null,\"stickied\":null,\"url\":null,\"subreddit_subscribers\":null,\"created_utc\":null,\"num_crossposts\":null,\"media\":null,\"is_video\":null}}";

            //var responseHeadersJson = "[{\"Key\":\"Connection\",\"Value\":[\"keep-alive\"]},{\"Key\":\"X-UA-Compatible\",\"Value\":[\"IE=edge\"]},{\"Key\":\"Cache-Control\",\"Value\":[\"no-store, must-revalidate, max-age=0, s-maxage=0, private\"]},{\"Key\":\"Access-Control-Allow-Origin\",\"Value\":[\"*\"]},{\"Key\":\"Access-Control-Expose-Headers\",\"Value\":[\"X-Moose\"]},{\"Key\":\"x-ratelimit-used\",\"Value\":[\"2\"]},{\"Key\":\"x-ratelimit-remaining\",\"Value\":[\"998.0\"]}]";
            //var responseHeaders = JsonConvert.DeserializeObject<HttpResponseHeaders>(responseHeadersJson);

            var redditListingResponse = JsonConvert.DeserializeObject<RedditListingResponse?>(redditListingResponseJson);

            return (redditListingResponse, default!);
        }

        public static HttpResponseHeaders GetHttpResponseHeaders()
        {
            var message = new HttpResponseMessage();
            var headers = message.Headers;
            headers.Add("HeaderKey", "HeaderValue");

            return headers;
        }

        #region Commands mock data
        public static AddPostsWithMostVotesCommand GetAddPostsWithMostVotesCommand(string subRedditName, string subRedditTimeFrameType, ushort limit = 25)
        {
            return new(subRedditName, subRedditTimeFrameType, limit);
        }

        public static List<InsertSubRedditPosts>? GetInsertSubredditPostsList()
        {
            var insertSubRedditPostsJson = "[{\"BatchId\":0,\"PostId\":\"1h8c3mw\",\"PostName\":\"t3_1h8c3mw\",\"PostTitle\":\"Why does alcohol burn when applied to a cut? And would 91% burn more than 70% or about the same? \",\"Ups\":2,\"Author\":\"Next_Conference1933\",\"PostCreatedUtc\":\"2024-12-06T21:21:44Z\",\"SubRedditName\":\"biology\",\"SubRedditTimeFrameType\":\"hour\",\"Limit\":2,\"CreatedDate\":\"2024-12-06T22:12:26.3105012Z\"}]";

            var insertSubRedditPostsList = JsonConvert.DeserializeObject<List<InsertSubRedditPosts>?>(insertSubRedditPostsJson);

            return insertSubRedditPostsList;
        }
        #endregion

        #region Service client data
        public static SubRedditTopRequest GetSubRedditTopRequest(string subRedditName, string subRedditTimeFrameType, ushort limit)
        {
            return new SubRedditTopRequest(subRedditName, subRedditTimeFrameType, limit);
        }
        #endregion
    }
}
