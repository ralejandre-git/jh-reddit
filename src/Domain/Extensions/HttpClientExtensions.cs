using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Domain.Extensions;
public static class HttpClientExtensions
{
    private static readonly JsonSerializerSettings JSON_SETTINGS = new JsonSerializerSettings
    {
        ContractResolver = new DefaultContractResolver
        {
            NamingStrategy = new CamelCaseNamingStrategy(),
        }
    };

    public static async Task<TResponse?> PostAsync<TResponse>(this HttpClient httpClient, string path, HttpContent content) where TResponse : class
    {
        var result = await httpClient.PostAsync(path, content);

        if (result != null)
        {
            result.EnsureSuccessStatusCode();
            string results = await result.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TResponse>(results);
        }

        return default;
    }

    public static async Task<TResponse?> PostAsync<TRequest, TResponse>(this HttpClient httpClient, string path, TRequest request) where TResponse : class
    {
        var json = JsonConvert.SerializeObject(request);
        var result = await httpClient.PostAsync(path, new StringContent(json, Encoding.UTF8, "application/json"));

        if (result != null)
        {
            result.EnsureSuccessStatusCode();
            string results = await result.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TResponse>(results);
        }

        return default;
    }

    public static async Task<(TResponse?, HttpResponseHeaders)> GetAsync<TResponse>(this HttpClient httpClient, string path) where TResponse : class
    {
        var result = await httpClient.GetAsync(path);

        result.EnsureSuccessStatusCode();
        string results = await result.Content.ReadAsStringAsync();

        return (JsonConvert.DeserializeObject<TResponse>(results), result.Headers);
    }
}
