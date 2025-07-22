using CommandLine;
using Grpc.Net.Client;
using GrpcClient;
using GrpcCalculatorService;
using Shared;


CalculatorGrpcClient CreateClient(CommonOptions commonOptions)
{
    var channel = GrpcChannel.ForAddress("http://localhost:5299");  // TODO: Dispose
    var client = new GrpcCalculator.GrpcCalculatorClient(channel);
    return new CalculatorGrpcClient(client);
}

return await CommandLine.Parser.Default.ParseArguments<AddOptions, SubtractOptions>(args).MapResult<AddOptions, Task<int>>(
    async (AddOptions addOptions) =>
    {
        var result = await CreateClient(addOptions).PerformCalculation(CalculatorOperation.Add, addOptions.OperandLeft, addOptions.OperandRight);
        Console.WriteLine(result);
        return 0;
    },
    errors => Task.FromResult(1)
);