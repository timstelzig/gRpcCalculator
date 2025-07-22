using GrpcServer.Services;
using Microsoft.Extensions.Logging.Abstractions;
using Shared;

namespace Tests;

public class CalculatorTests
{
    [Test]
    public void TestGreet()
    {
        var service = new CalculatorService(NullLogger<CalculatorService>.Instance);

        var result = service.PerformCalculation(CalculatorOperation.Add, 1, 2);
        
        Assert.That(result, Is.EqualTo(3));
    }
}