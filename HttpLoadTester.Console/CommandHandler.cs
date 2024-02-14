using System.Net;

namespace HttpLoadTester;

public class CommandHandler
{
    private readonly ILoadTester loadTester;

    public CommandHandler(ILoadTester loadTester)
    {
        this.loadTester = loadTester;
    }

    public async Task<string> Process(string[] args)
    {
        var uri = args[0].Split("=")[1].Trim();
        HttpStatusCode responseCode = await loadTester.SendGet(uri);
        return $"Response code: {responseCode}";
    }
}