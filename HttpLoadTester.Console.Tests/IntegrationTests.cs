using System.Text;
using HttpLoadTester.Console;

namespace HttpLoadTester.Tests.Integration;

public class IntegrationTests
{
    [Fact]
    public async void ConsoleApp_UserEntersUriAndSpecifiesOneRequest_PrintsOkResponseMessage()
    {
        var consoleOutput = new StringBuilder();
        var consoleOutputWriter = new StringWriter(consoleOutput);
        System.Console.SetOut(consoleOutputWriter);

        await Program.Main(["-Uri=http://localhost:5190/weatherforecast", "-Number=1"]);

        Assert.Equal("Response code: OK", consoleOutput.ToString().Split("\r\n")[0].Trim());
    }
}