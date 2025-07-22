using Grpc.Core;
using GrpcCalculatorService;
using GrpcServer.Services;
using Shared;

namespace GrpcServer.GRpcInterface;

public class GrpcCalculatorService : GrpcCalculator.GrpcCalculatorBase
{
    private readonly ILogger<GrpcCalculatorService> _logger;
    private readonly ICalculator _calculator;

    public GrpcCalculatorService(ILogger<GrpcCalculatorService> logger, ICalculator calculator)
    {
        _logger = logger;
        _calculator = calculator;
    }

    public override Task<GrpcCalculateReply> Calculate(GrpcCalculateRequest request, ServerCallContext context)
    {
        var operation = request.Operation switch
        {
            GrpcCalculatorOperation.Unspecified => throw new ArgumentOutOfRangeException(),
            GrpcCalculatorOperation.Add => CalculatorOperation.Add,
            GrpcCalculatorOperation.Subtract => CalculatorOperation.Subtract,
            GrpcCalculatorOperation.Multiply => CalculatorOperation.Multiply,
            GrpcCalculatorOperation.Divide => CalculatorOperation.Divide,
            _ => throw new ArgumentOutOfRangeException()
        };

        var calculationResult = _calculator.PerformCalculation(operation, request.LeftOperand, request.RightOperand);

        return Task.FromResult(new GrpcCalculateReply
        {
            Result = calculationResult
        });
    }
}