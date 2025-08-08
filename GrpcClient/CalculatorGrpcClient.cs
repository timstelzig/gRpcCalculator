using Grpc.Net.Client;
using GrpcCalculatorService;

namespace GrpcClient;

public interface ICalculatorClient
{
    public Task<double> Add(double leftSummand, double rightSummand);
    public Task<double> Subtract(double minuend, double subtrahend);
    public Task<double> Multiply(double leftFactor, double rightFactor);
    public Task<double> Divide(double dividend, double divisor);
}

public class CalculatorGrpcClient : ICalculatorClient
{
    private readonly GrpcCalculator.GrpcCalculatorClient _client;

    public CalculatorGrpcClient(GrpcChannel channel)
    {
        _client = new GrpcCalculator.GrpcCalculatorClient(channel);
    }

    public async Task<double> Add(double leftSummand, double rightSummand)
    {
        var request = new GrpcAddRequest { LeftSummand = leftSummand, RightSummand = rightSummand };
        var response = await _client.AddAsync(request);
        return response.Sum;
    }

    public async Task<double> Subtract(double minuend, double subtrahend)
    {
        var request = new GrpcSubtractRequest { Minuend = minuend, Subtrahend = subtrahend };
        var response = await _client.SubtractAsync(request);
        return response.Difference;
    }

    public async Task<double> Multiply(double leftFactor, double rightFactor)
    {
        var request = new GrpcMultiplyRequest { LeftFactor = leftFactor, RightFactor = rightFactor };
        var response = await _client.MultiplyAsync(request);
        return response.Product;
    }

    public async Task<double> Divide(double dividend, double divisor)
    {
        var request = new GrpcDivideRequest { Dividend = dividend, Divisor = divisor };
        var response = await _client.DivideAsync(request);
        return response.Quotient;
    }
}