using System.Net.Http.Json;

namespace eFriendOpenAPI.Extension;

public static class HttpClientExtension
{
    public static async Task<HttpResponseMessage> PostJsonContent(this HttpClient client, string url, object content)
    {
        var jsonContent = JsonContent.Create(content);
        await jsonContent.LoadIntoBufferAsync();
        return await client.PostAsync(url, jsonContent);
    }
}
