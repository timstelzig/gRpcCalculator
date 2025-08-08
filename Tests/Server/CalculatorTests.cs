using GrpcServer.Services;

namespace Tests.Server;

public class CalculatorTests
{
    private Calculator Instance => new ();
    
    [Test]
    public void TestAdd()
    {
        var result = Instance.Add(1, 2);
        Assert.That(result, Is.EqualTo(3));
    }
    
    [Test]
    public void TestSubtract()
    {
        var result = Instance.Subtract(3, 2);
        Assert.That(result, Is.EqualTo(1));
    }
    
    [Test]
    public void TestMultiply()
    {
        var result = Instance.Multiply(7, 8);
        Assert.That(result, Is.EqualTo(56));
    }
    
    [Test]
    public void TestDivide()
    {
        var result = Instance.Divide(8, 4);
        Assert.That(result, Is.EqualTo(2));
    }
    
    [Test]
    public void TestDivideByZero()
    {
        Assert.Throws<DivideByZeroException>(() => Instance.Divide(5, 0));
    }
}