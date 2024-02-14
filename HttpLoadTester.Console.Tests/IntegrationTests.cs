using System.Text;

namespace HttpLoadTester.Console.Tests;

public class IntegrationTests
{
    [Fact]
    public async void ConsoleApp_UserEntersUriAndSpecifiesOneRequest_PrintsOkResponseMessage()
    {
        var consoleOutput = new StringBuilder();
        var consoleOutputWriter = new StringWriter(consoleOutput);
        System.Console.SetOut(consoleOutputWriter);

        await Program.Main(["-Uri=http://localhost:5190/weatherforecast", "-Number=1"]);

        Assert.Equal("Request #1 - Response Status: OK", consoleOutput.ToString().Split("\n")[0].Trim());
    }

    [Fact]
    public async void ConsoleApp_UserEntersUriAndSpecifiesTwoRequests_PrintsTwoOkResponseMessages()
    {
        var consoleOutput = new StringBuilder();
        var consoleOutputWriter = new StringWriter(consoleOutput);
        System.Console.SetOut(consoleOutputWriter);

        await Program.Main(["-Uri=http://localhost:5190/weatherforecast", "-Number=2"]);

        Assert.Equal("Request #1 - Response Status: OK", consoleOutput.ToString().Split("\n")[0].Trim());
        Assert.Equal("Request #2 - Response Status: OK", consoleOutput.ToString().Split("\n")[1].Trim());
    }
}