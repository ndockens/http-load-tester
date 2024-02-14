using System.Net;
using System.Text;

namespace HttpLoadTester;

public class CommandHandler
{
    private readonly ILoadTester loadTester;
    private string uri = "";
    private int numberOfRequests = 0;

    public CommandHandler(ILoadTester loadTester)
    {
        this.loadTester = loadTester;
    }

    public async Task<string> Process(string[] args)
    {
        ThrowExceptionIfEmpty(args);

        var resultMessageBuilder = new StringBuilder();

        ParseArguments(args);
        List<HttpStatusCode> results = await loadTester.SendGet(uri, numberOfRequests);

        for (int i = 0; i < results.Count; i++)
        {
            resultMessageBuilder.AppendLine($"Request #{i + 1} - Response Status: {results[i]}");
        }

        return resultMessageBuilder.ToString();
    }

    private void ThrowExceptionIfEmpty(string[] args)
    {
        if (args.Length == 0)
        {
            throw new ArgumentException("Cannot be an empty array.", nameof(args));
        }
    }

    private void ParseArguments(string[] args)
    {
        uri = ParseUri(args);
        numberOfRequests = ParseNumberOfRequests(args);
    }

    private string ParseUri(string[] args)
    {
        return args[0].Split("=")[1];
    }

    private int ParseNumberOfRequests(string[] args)
    {
        if (args.Length < 2)
            return 1;

        var numberOfRequestsString = args[1].Split("=")[1];
        var numberOfRequests = int.Parse(numberOfRequestsString);

        return numberOfRequests;
    }
}