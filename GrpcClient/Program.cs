using CommandLine;
using Grpc.Core;
using GrpcClient;

return await CliParser.ParseArgs(args).MapResult(
    async cliOptions => await RunCalculation(cliOptions),
    _ => Task.FromResult(2)
);


async Task<int> RunCalculation(CliOptions cliOptions)
{
    try
    {
        using var channel = GrpcChannelCreator.CreateChannel(cliOptions.GetConnectionOptions());
        var client = new CalculatorGrpcClient(channel);
        var calculator = new CalculatorCliClient(client);
        Console.WriteLine(await calculator.RunCalculation(cliOptions));
        return 0;
    }
    catch (RpcException e)
    {
        Console.WriteLine($"{e.Status.StatusCode}: {e.Status.Detail}");
        return 1;
    }
}