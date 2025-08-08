using Grpc.Core;
using GrpcCalculatorService;
using GrpcServer.Services;

namespace GrpcServer.GRpcInterface;

internal class GrpcCalculator(ICalculator calculator)
    : GrpcCalculatorService.GrpcCalculator.GrpcCalculatorBase
{
    public override Task<GrpcAddReply> Add(GrpcAddRequest request, ServerCallContext context)
    {
        return Task.FromResult(new GrpcAddReply { Sum = calculator.Add(request.LeftSummand, request.RightSummand) });
    }

    public override Task<GrpcSubtractReply> Subtract(GrpcSubtractRequest request, ServerCallContext context)
    {
        return Task.FromResult(new GrpcSubtractReply
            { Difference = calculator.Subtract(request.Minuend, request.Subtrahend) });
    }

    public override Task<GrpcMultiplyReply> Multiply(GrpcMultiplyRequest request, ServerCallContext context)
    {
        return Task.FromResult(new GrpcMultiplyReply
            { Product = calculator.Multiply(request.LeftFactor, request.RightFactor) });
    }

    public override Task<GrpcDivideReply> Divide(GrpcDivideRequest request, ServerCallContext context)
    {
        try
        {
            return Task.FromResult(new GrpcDivideReply
                { Quotient = calculator.Divide(request.Dividend, request.Divisor) });
        }
        catch (DivideByZeroException)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Divisor must be non-zero"));
        }
    }
}