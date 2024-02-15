using System.Net;

namespace HttpLoadTester.Core.Tests.Integration;

public class HttpServiceTests
{
    private static readonly string testApiBaseUri = "http://localhost:5190";
    private static readonly string weatherForecastUri = $"{testApiBaseUri}/weatherforecast";
    private static readonly string invalidUri = $"{testApiBaseUri}/invalid";

    [Fact]
    public async void Get_ValidUri_ReturnsHttpOkResponse()
    {
        var httpService = new HttpService();

        var response = await httpService.Get(weatherForecastUri);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async void Get_InvalidUri_ReturnsHttpNotFoundResponse()
    {
        var httpService = new HttpService();

        var response = await httpService.Get(invalidUri);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}