using Grpc.Core;
using GrpcCalculatorService;
using GrpcServer.Services;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace Tests.Server;

public class GrpcCalculatorTests
{
    private GrpcServer.GRpcInterface.GrpcCalculatorService GetService(ICalculator calculatorMockObject) => 
        new (
            NullLogger<GrpcServer.GRpcInterface.GrpcCalculatorService>.Instance,
            calculatorMockObject);
    
    [Test]
    public async Task TestAdd()
    {
        var calculatorMock = new Mock<ICalculator>();
        calculatorMock.Setup(calculator => calculator.Add(1, 2)).Returns(3);
        var context = Mock.Of<ServerCallContext>();
        var grpcService = GetService(calculatorMock.Object);

        var reply = await grpcService.Add(new GrpcAddRequest { LeftSummand = 1, RightSummand = 2 }, context);

        Assert.That(reply.Sum, Is.EqualTo(3));
    }
    
    [Test]
    public async Task TestSubtract()
    {
        var calculatorMock = new Mock<ICalculator>();
        calculatorMock.Setup(calculator => calculator.Subtract(3, 2)).Returns(1);
        var context = Mock.Of<ServerCallContext>();
        var grpcService = GetService(calculatorMock.Object);

        var reply = await grpcService.Subtract(new GrpcSubtractRequest { Minuend = 3, Subtrahend = 2 }, context);

        Assert.That(reply.Difference, Is.EqualTo(1));
    }
    
    [Test]
    public async Task TestMultiply()
    {
        var calculatorMock = new Mock<ICalculator>();
        calculatorMock.Setup(calculator => calculator.Multiply(7, 8)).Returns(56);
        var context = Mock.Of<ServerCallContext>();
        var grpcService = GetService(calculatorMock.Object);

        var reply = await grpcService.Multiply(new GrpcMultiplyRequest { LeftFactor = 7, RightFactor = 8 }, context);

        Assert.That(reply.Product, Is.EqualTo(56));
    }
    
    [Test]
    public async Task TestDivide()
    {
        var calculatorMock = new Mock<ICalculator>();
        calculatorMock.Setup(calculator => calculator.Divide(8, 4)).Returns(2);
        var context = Mock.Of<ServerCallContext>();
        var grpcService = GetService(calculatorMock.Object);

        var reply = await grpcService.Divide(new GrpcDivideRequest { Dividend = 8, Divisor = 4 }, context);

        Assert.That(reply.Quotient, Is.EqualTo(2));
    }
    
    [Test]
    public async Task TestDivideByZero()
    {
        var calculatorMock = new Mock<ICalculator>();
        calculatorMock.Setup(calculator => calculator.Divide(8, 0)).Throws<DivideByZeroException>();
        var context = Mock.Of<ServerCallContext>();
        var grpcService = GetService(calculatorMock.Object);

        var exception = Assert.ThrowsAsync<RpcException>(async () => await grpcService.Divide(new GrpcDivideRequest{Dividend = 8,Divisor = 0}, context));
        Assert.That(exception.Status.StatusCode, Is.EqualTo(StatusCode.InvalidArgument));
        Assert.That(exception.Status.Detail, Is.EqualTo("Divisor must be non-zero"));
    }
}