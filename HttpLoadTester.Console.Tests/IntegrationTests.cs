using System.Text;

namespace HttpLoadTester.Console.Tests;

public class IntegrationTests
{
    private static readonly string testApiBaseUri = "http://localhost:5190";
    private static readonly string weatherForecastUri = $"{testApiBaseUri}/weatherforecast";

    [Fact]
    public async void ConsoleApp_UserEntersUri_PrintsOkResponseMessage()
    {
        var consoleOutput = new StringBuilder();
        var consoleOutputWriter = new StringWriter(consoleOutput);
        System.Console.SetOut(consoleOutputWriter);

        await Program.Main([$"-Uri={weatherForecastUri}"]);

        Assert.Equal("Request #1 - Response Status: OK", consoleOutput.ToString().Split("\n")[0].Trim());
    }

    [Fact]
    public async void ConsoleApp_UserEntersUriAndSpecifiesTwoRequests_PrintsTwoOkResponseMessages()
    {
        var consoleOutput = new StringBuilder();
        var consoleOutputWriter = new StringWriter(consoleOutput);
        System.Console.SetOut(consoleOutputWriter);

        await Program.Main([$"-Uri={weatherForecastUri}", "-NumberOfRequests=2"]);

        Assert.Equal("Request #1 - Response Status: OK", consoleOutput.ToString().Split("\n")[0].Trim());
        Assert.Equal("Request #2 - Response Status: OK", consoleOutput.ToString().Split("\n")[1].Trim());
    }
}