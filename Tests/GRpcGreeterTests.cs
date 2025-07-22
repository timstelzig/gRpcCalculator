using Grpc.Core;
using GrpcGreeterService;
using GrpcServer.Services;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace Tests;

public class GrpcGreeterTests
{
    [Test]
    public async Task TestGreet()
    {
        var greeterMock = new Mock<IGreeterService>();
        greeterMock.Setup(greeter => greeter.SayHello("Kevin")).Returns(Task.FromResult("Hello Kevin"));
        var context = Mock.Of<ServerCallContext>();
        
        var grpcService = new GrpcServer.GRpcInterface.GrpcGreeterService(NullLogger<GreeterService>.Instance, greeterMock.Object);

        var result = await grpcService.SayHello(new HelloRequest{Name = "Kevin"}, context);
        
        Assert.That(result.Message, Is.EqualTo("Hello Kevin"));
    }
}