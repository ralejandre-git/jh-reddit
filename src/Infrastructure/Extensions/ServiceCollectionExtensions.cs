using Infrastructure.Abstractions;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;
using System.Text;

namespace Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddReditServices(this IServiceCollection services, string redditBaseUrl, string redditTokenBaseUrl, string authorizationMethod = "Basic", string authorizationToken = "")
    {
        services.AddHttpClient<IRedditTokenService, RedditTokenService>((provider, client) =>
        {
            /*
            <platform>:<app ID>:<version string> (by /u/<reddit username>)
            Example:
            User-Agent: android:com.example.myredditapp:v1.2.3 (by /u/kemitche)
             */
            client.SetUserAgent();
            client.BaseAddress = new Uri(redditTokenBaseUrl);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(authorizationMethod, authorizationToken);
        });

        services.AddHttpClient<IRedditServiceClient, RedditServiceClient>((provider, client) =>
        {
            client.SetUserAgent();
            client.BaseAddress = new Uri(redditBaseUrl);
        });

        return services;
    }

    private static void SetUserAgent(this HttpClient client)
    {
        client.DefaultRequestHeaders.UserAgent.Clear();
        client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("JH.Reddit", "1.0.0"));
        client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("(by /u/ralejandre)"));
    }
}
