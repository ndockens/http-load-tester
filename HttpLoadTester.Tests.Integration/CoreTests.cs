using System.Net;

namespace HttpLoadTester.Tests.Integration;

public class CoreTests
{
    private static readonly string testApiBaseUri = "http://localhost:5190";
    private static readonly string weatherForecastUri = $"{testApiBaseUri}/weatherforecast";
    private static readonly string invalidUri = $"{testApiBaseUri}/invalid";

    [Fact]
    public async void LoadTester_SendGetRequest_ReturnsOkResponse()
    {
        var loadTester = new LoadTester();

        HttpStatusCode responseCode = await loadTester.SendGet(weatherForecastUri);

        Assert.Equal(HttpStatusCode.OK, responseCode);
    }

    [Fact]
    public async void LoadTester_SendGetRequestToInvalidUri_ReturnsNotFoundResponse()
    {
        var loadTester = new LoadTester();

        HttpStatusCode responseCode = await loadTester.SendGet(invalidUri);

        Assert.Equal(HttpStatusCode.NotFound, responseCode);
    }
}