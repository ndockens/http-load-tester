using System.Net;

namespace HttpLoadTester.Core;

public interface IHttpService
{
    public Task<HttpResponseMessage> Get(string uri);
}