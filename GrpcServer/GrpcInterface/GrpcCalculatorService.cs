using Grpc.Core;
using GrpcCalculatorService;
using GrpcServer.Services;

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

    public override Task<GrpcAddReply> Add(GrpcAddRequest request, ServerCallContext context)
    {
        return Task.FromResult(new GrpcAddReply { Sum = _calculator.Add(request.LeftSummand, request.RightSummand) });
    }

    public override Task<GrpcSubtractReply> Subtract(GrpcSubtractRequest request, ServerCallContext context)
    {
        return Task.FromResult(new GrpcSubtractReply
            { Difference = _calculator.Subtract(request.Minuend, request.Subtrahend) });
    }

    public override Task<GrpcMultiplyReply> Multiply(GrpcMultiplyRequest request, ServerCallContext context)
    {
        return Task.FromResult(new GrpcMultiplyReply
            { Product = _calculator.Multiply(request.LeftFactor, request.RightFactor) });
    }

    public override Task<GrpcDivideReply> Divide(GrpcDivideRequest request, ServerCallContext context)
    {
        try
        {
            return Task.FromResult(new GrpcDivideReply
                { Quotient = _calculator.Divide(request.Dividend, request.Divisor) });
        }
        catch (DivideByZeroException)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Divisor must be non-zero"));
        }
    }
}