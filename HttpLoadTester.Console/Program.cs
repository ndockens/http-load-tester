using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("HttpLoadTester.Console.Tests")]
namespace HttpLoadTester.Console;

internal class Program
{
    public static async Task Main(string[] args)
    {
        var loadTester = new LoadTester();
        var commandHandler = new CommandHandler(loadTester);

        string resultMessage = await commandHandler.Process(args);

        System.Console.WriteLine(resultMessage);
    }
}
