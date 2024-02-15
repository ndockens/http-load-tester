using System.Net;
using System.Text;

namespace HttpLoadTester;

public class CommandHandler
{
    private readonly int defaultNumberOfRequests = 1;
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
        ParseUri(args);
        ParseNumberOfRequests(args);
    }

    private void ParseUri(string[] args)
    {
        uri = ParseArgumentValue("Uri", args);
    }

    private void ParseNumberOfRequests(string[] args)
    {
        string numberOfRequestsString = ParseArgumentValue("NumberOfRequests", args);
        int.TryParse(numberOfRequestsString, out numberOfRequests);

        if (numberOfRequests <= 0)
            numberOfRequests = defaultNumberOfRequests;
    }

    private string ParseArgumentValue(string argumentName, string[] args)
    {
        string? argument = args.FirstOrDefault(x => x.StartsWith($"-{argumentName}="));

        if (string.IsNullOrEmpty(argument))
            return "";

        string argumentValue = argument.Split("=")[1];

        return argumentValue;
    }
}