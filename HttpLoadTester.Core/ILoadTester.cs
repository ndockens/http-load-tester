using System.Net;

namespace HttpLoadTester;

public interface ILoadTester
{
    public Task<List<HttpStatusCode>> SendGet(string uri, int numberOfRequests);
}