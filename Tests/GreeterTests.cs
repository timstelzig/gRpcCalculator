using GrpcServer.Services;
using Microsoft.Extensions.Logging.Abstractions;

namespace Tests;

public class GreeterTests
{
    [Test]
    public async Task TestGreet()
    {
        var service = new GreeterService(NullLogger<GreeterService>.Instance);

        var result = await service.SayHello("Kevin");
        
        Assert.That(result, Is.EqualTo("Hello Kevin"));
    }
}