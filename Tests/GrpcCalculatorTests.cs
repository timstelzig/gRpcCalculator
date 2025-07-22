using Grpc.Core;
using GrpcCalculatorService;
using GrpcServer.Services;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Shared;

namespace Tests;

public class GrpcCalculatorTests
{
    [Test]
    public async Task TestGreet()
    {
        var calculatorMock = new Mock<ICalculator>();
        calculatorMock.Setup(greeter => greeter.PerformCalculation(CalculatorOperation.Add, 1, 2)).Returns(3);
        var context = Mock.Of<ServerCallContext>();

        var grpcService =
            new GrpcServer.GRpcInterface.GrpcCalculatorService(NullLogger<GrpcServer.GRpcInterface.GrpcCalculatorService>.Instance,
                calculatorMock.Object);

        var reply = await grpcService.Calculate(new GrpcCalculateRequest { Operation = GrpcCalculatorOperation.Add, LeftOperand = 1, RightOperand = 2}, context);

        Assert.That(reply.Result, Is.EqualTo(3));
    }
}