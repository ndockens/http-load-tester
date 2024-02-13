using System.Net;

namespace HttpLoadTester;

public interface ILoadTester
{
    public Task<HttpStatusCode> SendGet(string uri);
}