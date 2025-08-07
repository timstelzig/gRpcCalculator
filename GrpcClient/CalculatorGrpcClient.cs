using Grpc.Net.Client;
using GrpcCalculatorService;
using Shared;

namespace GrpcClient;

public interface ICalculatorClient
{
    Task<double> PerformCalculation(CalculatorOperation operation, double left, double right);
}

public class CalculatorGrpcClient : ICalculatorClient
{
    private readonly GrpcCalculator.GrpcCalculatorClient _client;

    public CalculatorGrpcClient(GrpcChannel channel)
    {
        _client = new GrpcCalculator.GrpcCalculatorClient(channel);
    }

    public async Task<double> PerformCalculation(CalculatorOperation operation, double left, double right)
    {
        var grpcOperation = operation switch
        {
            CalculatorOperation.Add => GrpcCalculatorOperation.Add,
            CalculatorOperation.Subtract => GrpcCalculatorOperation.Subtract,
            CalculatorOperation.Multiply => GrpcCalculatorOperation.Multiply,
            CalculatorOperation.Divide => GrpcCalculatorOperation.Divide
        };
        var request = new GrpcCalculateRequest { Operation = grpcOperation, LeftOperand = left, RightOperand = right };
        var response = await _client.CalculateAsync(request);
        return response.Result;
    }
}