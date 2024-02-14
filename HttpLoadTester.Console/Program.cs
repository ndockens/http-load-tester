using System.Runtime.CompilerServices;
using HttpLoadTester.Core;

[assembly: InternalsVisibleTo("HttpLoadTester.Console.Tests")]
namespace HttpLoadTester.Console;

internal class Program
{
    public static async Task Main(string[] args)
    {
        var httpService = new HttpService();
        var loadTester = new LoadTester(httpService);
        var commandHandler = new CommandHandler(loadTester);

        string resultMessage = await commandHandler.Process(args);

        System.Console.WriteLine(resultMessage);
    }
}
