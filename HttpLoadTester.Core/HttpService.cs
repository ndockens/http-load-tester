namespace HttpLoadTester.Core;

public class HttpService : IHttpService
{
    public async Task<HttpResponseMessage> Get(string uri)
    {
        var client = new HttpClient();
        HttpResponseMessage response = await client.GetAsync(uri);

        return response;
    }
}