using System.Runtime.CompilerServices;
using HttpLoadTester.Core;

[assembly: InternalsVisibleTo("HttpLoadTester.Console.Tests")]
namespace HttpLoadTester.Console;

internal class Program
{
    private static IHttpService? httpService;
    private static ILoadTester? loadTester;
    private static CommandHandler? commandHandler; // TODO: Create an interface for CommandHandler
    private static string resultMessage = "";

    public static async Task Main(string[] args)
    {
        Setup();
        await Run(args);
        WriteOutput();
    }

    private static void Setup()
    {
        httpService = new HttpService();
        loadTester = new LoadTester(httpService);
        commandHandler = new CommandHandler(loadTester);
    }

    private static async Task Run(string[] args)
    {
        if (commandHandler == null)
            return;

        resultMessage = await commandHandler.Process(args);
    }

    private static void WriteOutput()
    {
        System.Console.WriteLine(resultMessage);
    }
}
