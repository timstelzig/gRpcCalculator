using System.Globalization;
using GrpcClient;
using Moq;

namespace Tests.Client;

public class CliClient
{
    [Test]
    public async Task TestAdd()
    {
        var mock = new Mock<ICalculatorClient>();
        mock.Setup(calculator => calculator.Add(1, 2)).Returns(Task.FromResult<double>(3));
        var service = new CalculatorCliClient(mock.Object, CultureInfo.InvariantCulture);
        
        var result = await service.RunCalculation(new CliOptions
            { Operation = CalculatorOperation.Add, OperandLeft = 1, OperandRight = 2 });

        Assert.That(result, Is.EqualTo("3"));
    }
    
    [Test]
    public async Task TestSubtract()
    {
        var mock = new Mock<ICalculatorClient>();
        mock.Setup(calculator => calculator.Subtract(3, 2)).Returns(Task.FromResult<double>(1));
        var service = new CalculatorCliClient(mock.Object, CultureInfo.InvariantCulture);
        
        var result = await service.RunCalculation(new CliOptions
            { Operation = CalculatorOperation.Subtract, OperandLeft = 3, OperandRight = 2 });

        Assert.That(result, Is.EqualTo("1"));
    }
    
    [Test]
    public async Task TestMultiply()
    {
        var mock = new Mock<ICalculatorClient>();
        mock.Setup(calculator => calculator.Multiply(7, 8)).Returns(Task.FromResult<double>(56));
        var service = new CalculatorCliClient(mock.Object, CultureInfo.InvariantCulture);
        
        var result = await service.RunCalculation(new CliOptions
            { Operation = CalculatorOperation.Multiply, OperandLeft = 7, OperandRight = 8 });

        Assert.That(result, Is.EqualTo("56"));
    }
    
    [Test]
    public async Task TestDivide()
    {
        var mock = new Mock<ICalculatorClient>();
        mock.Setup(calculator => calculator.Divide(8, 4)).Returns(Task.FromResult<double>(2));
        var service = new CalculatorCliClient(mock.Object, CultureInfo.InvariantCulture);
        
        var result = await service.RunCalculation(new CliOptions
            { Operation = CalculatorOperation.Divide, OperandLeft = 8, OperandRight = 4 });

        Assert.That(result, Is.EqualTo("2"));
    }
}