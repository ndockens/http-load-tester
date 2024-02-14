using System.Net;
using NSubstitute;

namespace HttpLoadTester.Tests.Integration;

public class CommandHandlerTests
{
    private readonly string testUri = "http://www.test.com";
    private ILoadTester loadTester;
    private CommandHandler commandHandler;

    private void InitializeLoadTester()
    {
        loadTester = Substitute.For<ILoadTester>();
    }

    private void InitializeCommandHandler()
    {
        commandHandler = new CommandHandler(loadTester);
    }

    [Fact]
    public async void Process_GetCommandWithUriArgument_InvokesLoadTesterToSendGetRequestToUri()
    {
        InitializeLoadTester();
        InitializeCommandHandler();

        await commandHandler.Process([$"-Uri={testUri}"]);

        await loadTester.Received().SendGet(testUri);
    }

    [Fact]
    public async void Process_GetCommandWithUriArgument_ReturnsResponseCodeMessage()
    {
        InitializeLoadTester();
        loadTester.SendGet(testUri).Returns(HttpStatusCode.OK);
        InitializeCommandHandler();

        string resultMessage = await commandHandler.Process([$"-Uri={testUri}"]);

        Assert.Equal($"Response code: {HttpStatusCode.OK}", resultMessage);
    }
}