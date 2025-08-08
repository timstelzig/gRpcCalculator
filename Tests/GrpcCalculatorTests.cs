using Grpc.Core;
using GrpcCalculatorService;
using GrpcServer.Services;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace Tests;

public class GrpcCalculatorTests
{
    [Test]
    public async Task TestGreet()
    {
        var calculatorMock = new Mock<ICalculator>();
        calculatorMock.Setup(greeter => greeter.Add(1, 2)).Returns(3);
        var context = Mock.Of<ServerCallContext>();

        var grpcService =
            new GrpcServer.GRpcInterface.GrpcCalculatorService(
                NullLogger<GrpcServer.GRpcInterface.GrpcCalculatorService>.Instance,
                calculatorMock.Object);

        var reply = await grpcService.Add(new GrpcAddRequest { LeftSummand = 1, RightSummand = 2 }, context);

        Assert.That(reply.Sum, Is.EqualTo(3));
    }
}