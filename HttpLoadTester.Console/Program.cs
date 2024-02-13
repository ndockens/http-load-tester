using HttpLoadTester;

var loadTester = new LoadTester();
var commandHandler = new CommandHandler(loadTester);

string resultMessage = await commandHandler.Process(args);

Console.WriteLine(resultMessage);
