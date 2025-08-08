using GrpcServer.Services;

namespace Tests.Server;

/// <summary>
/// Test that calculation are correct.
/// </summary>
public class CalculatorTests
{
    [Test]
    public void TestAdd()
    {
        var result = new Calculator().Add(1, 2);
        Assert.That(result, Is.EqualTo(3));
    }

    [Test]
    public void TestSubtract()
    {
        var result = new Calculator().Subtract(3, 2);
        Assert.That(result, Is.EqualTo(1));
    }

    [Test]
    public void TestMultiply()
    {
        var result = new Calculator().Multiply(7, 8);
        Assert.That(result, Is.EqualTo(56));
    }

    [Test]
    public void TestDivide()
    {
        var result = new Calculator().Divide(8, 4);
        Assert.That(result, Is.EqualTo(2));
    }

    [Test]
    public void TestDivideByZero()
    {
        Assert.Throws<DivideByZeroException>(() => new Calculator().Divide(5, 0));
    }
}