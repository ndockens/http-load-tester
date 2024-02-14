using System.Net;
using HttpLoadTester.Core;
using NSubstitute;

namespace HttpLoadTester.Tests.Integration;

public class LoadTesterTests
{
    private readonly string testUri = "http://www.test.com";
    private readonly IHttpService httpService;
    private readonly LoadTester loadTester;

    public LoadTesterTests()
    {
        httpService = Substitute.For<IHttpService>();
        loadTester = new LoadTester(httpService);
    }

    private void SetUpHttpServiceToAlwaysReturnOk()
    {
        httpService.Get(Arg.Any<string>()).Returns(new HttpResponseMessage(HttpStatusCode.OK));
    }

    [Fact]
    public async void LoadTester_SendOneGetRequest_ReturnsOkResponse()
    {
        SetUpHttpServiceToAlwaysReturnOk();

        List<HttpStatusCode> responseCodes = await loadTester.SendGet(testUri, 1);

        Assert.Single(responseCodes);
        Assert.Equal(HttpStatusCode.OK, responseCodes[0]);
    }

    [Fact]
    public async void LoadTester_SendTwoGetRequests_ReturnsTwoOkResponses()
    {
        SetUpHttpServiceToAlwaysReturnOk();

        List<HttpStatusCode> responseCodes = await loadTester.SendGet(testUri, 2);

        Assert.Equal(2, responseCodes.Count);
        Assert.Equal(HttpStatusCode.OK, responseCodes[0]);
        Assert.Equal(HttpStatusCode.OK, responseCodes[1]);
    }
}