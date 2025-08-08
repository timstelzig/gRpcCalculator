using GrpcServer.Services;
using Microsoft.Extensions.Logging.Abstractions;

namespace Tests;

public class CalculatorTests
{
    [Test]
    public void TestAdd()
    {
        var service = new CalculatorService(NullLogger<CalculatorService>.Instance);
        var result = service.Add(1, 2);
        Assert.That(result, Is.EqualTo(3));
    }
}