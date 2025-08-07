using CommandLine;
using GrpcClient;

return await CliParser.ParseArgs(args).MapResult(async cliOptions =>
{
    await RunCalculation(cliOptions);
    return 0;
}, _ => Task.FromResult(1));


async Task RunCalculation(CliOptions cliOptions)
{
    using var channel = GrpcChannelCreator.CreateChannel(cliOptions.GetConnectionOptions());
    var client = new CalculatorGrpcClient(channel);
    var calculator = new CalculatorCliClient(client);
    Console.WriteLine(await calculator.RunCalculation(cliOptions));
}