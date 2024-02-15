using System.Net;

namespace HttpLoadTester.Core;

public class LoadTester : ILoadTester
{
    private readonly IHttpService httpService;

    public LoadTester(IHttpService httpService)
    {
        this.httpService = httpService;
    }

    public async Task<List<HttpStatusCode>> SendGet(string uri, int numberOfRequests)
    {
        var responseStatusCodes = new List<HttpStatusCode>();

        for (int i = 0; i < numberOfRequests; i++)
        {
            HttpResponseMessage response = await httpService.Get(uri);
            responseStatusCodes.Add(response.StatusCode);
        }

        return responseStatusCodes;
    }
}