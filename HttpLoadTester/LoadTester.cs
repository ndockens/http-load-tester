using System.Net;

namespace HttpLoadTester;

public class LoadTester : ILoadTester
{
    public async Task<HttpStatusCode> SendGet(string uri)
    {
        var client = new HttpClient();
        var response = await client.GetAsync(uri);
        return response.StatusCode;
    }
}