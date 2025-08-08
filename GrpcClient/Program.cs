using CommandLine;
using Grpc.Core;
using GrpcClient;

return await CliParser.ParseArgs(args).MapResult(
    HandleCliOptions,
    _ => Task.FromResult(2)
);


async Task<int> HandleCliOptions(CliOptions cliOptions)
{
    try
    {
        using var grpcChannel = GrpcChannelCreator.CreateChannel(cliOptions.GetConnectionOptions());
        var grpcClient = new CalculatorGrpcClient(grpcChannel);
        var cliHandler = new CliCalculator(grpcClient);
        Console.WriteLine(await cliHandler.RunCalculation(cliOptions));
        return 0;
    }
    catch (RpcException e)
    {
        // This includes connection issues, as well as logic issues in the server handler
        Console.WriteLine($"{e.Status.StatusCode}: {e.Status.Detail}");
        return 1;
    }
}