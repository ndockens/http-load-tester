using System.Net;
using NSubstitute;

namespace HttpLoadTester.Console.Tests.Unit;

public class CommandHandlerTests
{
    private readonly string testUri = "http://www.test.com";
    private readonly ILoadTester loadTester;
    private readonly CommandHandler commandHandler;

    public CommandHandlerTests()
    {
        loadTester = Substitute.For<ILoadTester>();
        commandHandler = new CommandHandler(loadTester);
    }

    private void SetLoadTesterReturnValue(string methodName, List<HttpStatusCode> returnValue)
    {
        if (methodName == "SendGet")
        {
            loadTester.SendGet(Arg.Any<string>(), Arg.Any<int>()).Returns(returnValue);
        }
    }

    [Fact]
    public async void Process_NoArguments_ThrowsArgumentExceptionWithParameterName()
    {
        var parameterName = "args";
        await Assert.ThrowsAsync<ArgumentException>(parameterName, () => commandHandler.Process([]));
    }

    [Fact]
    public async void Process_ArgumentsContainUri_InvokesLoadTesterToSendGetRequestToUri()
    {
        SetLoadTesterReturnValue("SendGet", [HttpStatusCode.OK]);
        string[] arguments = [$"-Uri={testUri}"];

        await commandHandler.Process(arguments);

        await loadTester.Received(1).SendGet(testUri, 1);
    }

    [Fact]
    public async void Process_ArgumentsContainUri_ReturnsResponseStatusMessage()
    {
        SetLoadTesterReturnValue("SendGet", [HttpStatusCode.OK]);
        string[] arguments = [$"-Uri={testUri}"];
        var expectedResultMessage = "Request #1 - Response Status: OK\n";

        string resultMessage = await commandHandler.Process(arguments);

        Assert.Equal(expectedResultMessage, resultMessage);
    }

    [Fact]
    public async void Process_ArgumentsContainUriAndNumberOfRequestsEqualTo2_InvokesLoadTesterToSendTwoGetRequestsToUri()
    {
        SetLoadTesterReturnValue("SendGet", [HttpStatusCode.OK, HttpStatusCode.OK]);
        string[] arguments = [$"-Uri={testUri}", "-NumberOfRequests=2"];

        await commandHandler.Process(arguments);

        await loadTester.Received(1).SendGet(testUri, 2);
    }

    [Fact]
    public async void Process_ArgumentsContainUriAndNumberOfRequestsEqualTo2_ReturnsResponseStatusMessagesForTwoRequests()
    {
        SetLoadTesterReturnValue("SendGet", [HttpStatusCode.OK, HttpStatusCode.OK]);
        string[] arguments = [$"-Uri={testUri}", "-NumberOfRequests=2"];
        var expectedResultMessage = "Request #1 - Response Status: OK\nRequest #2 - Response Status: OK\n";

        string resultMessage = await commandHandler.Process(arguments);

        Assert.Equal(expectedResultMessage, resultMessage);
    }

    [Fact]
    public async void Process_ArgumentsAreOutOfOrder_InvokesLoadTesterToSendGetRequestToUri()
    {
        SetLoadTesterReturnValue("SendGet", [HttpStatusCode.OK]);
        string[] arguments = ["-NumberOfRequests=1", $"-Uri={testUri}"];

        await commandHandler.Process(arguments);

        await loadTester.Received(1).SendGet(testUri, 1);
    }
}