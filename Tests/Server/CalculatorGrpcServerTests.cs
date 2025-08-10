using Grpc.Core;
using GrpcCalculatorService;
using GrpcServer.Services;
using Moq;

namespace Tests.Server;

/// <summary>
/// Test that the GrpcCalculator correctly forwards requests to the calculator, and handles results.
/// </summary>
public class CalculatorGrpcServerTests
{
    private static ServerCallContext Context => Mock.Of<ServerCallContext>();
    private readonly Mock<ICalculator> _calculator = new();
    
    private GrpcServer.GRpcInterface.CalculatorGrpcServer GetService() =>
        new(_calculator.Object);

    [Test]
    public async Task TestAdd()
    {
        _calculator.Setup(calculator => calculator.Add(1, 2)).Returns(3);

        var reply = await GetService().Add(new GrpcAddRequest { LeftSummand = 1, RightSummand = 2 }, Context);

        Assert.That(reply.Sum, Is.EqualTo(3));
    }

    [Test]
    public async Task TestSubtract()
    {
        _calculator.Setup(calculator => calculator.Subtract(3, 2)).Returns(1);

        var reply = await GetService().Subtract(new GrpcSubtractRequest { Minuend = 3, Subtrahend = 2 }, Context);

        Assert.That(reply.Difference, Is.EqualTo(1));
    }

    [Test]
    public async Task TestMultiply()
    {
        _calculator.Setup(calculator => calculator.Multiply(7, 8)).Returns(56);

        var reply = await GetService().Multiply(new GrpcMultiplyRequest { LeftFactor = 7, RightFactor = 8 }, Context);

        Assert.That(reply.Product, Is.EqualTo(56));
    }

    [Test]
    public async Task TestDivide()
    {
        _calculator.Setup(calculator => calculator.Divide(8, 4)).Returns(2);

        var reply = await GetService().Divide(new GrpcDivideRequest { Dividend = 8, Divisor = 4 }, Context);

        Assert.That(reply.Quotient, Is.EqualTo(2));
    }

    [Test]
    public void TestDivideByZero()
    {
        _calculator.Setup(calculator => calculator.Divide(8, 0)).Throws<DivideByZeroException>();

        var exception = Assert.ThrowsAsync<RpcException>(async () =>
            await GetService().Divide(new GrpcDivideRequest { Dividend = 8, Divisor = 0 }, Context));
        
        Assert.That(exception.Status.StatusCode, Is.EqualTo(StatusCode.InvalidArgument));
        Assert.That(exception.Status.Detail, Is.EqualTo("Divisor must be non-zero"));
    }
}