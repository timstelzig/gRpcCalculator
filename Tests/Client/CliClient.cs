using System.Globalization;
using GrpcClient;
using Moq;

namespace Tests.Client;

/// <summary>
/// Test that the CliHandler correctly forwards requests to the remote service, and handles results.
/// </summary>
public class CliClient
{
    private readonly Mock<IRemoteCalculatorClient> _calculator = new();
    private CliCalculator GetCliCalculator() => new(_calculator.Object, CultureInfo.InvariantCulture);

    private async Task<string> Calculate(MathOperator operation, double op1, double op2) => await GetCliCalculator()
        .RunCalculation(new CliOptions
        {
            Operation = operation, OperandLeft = op1, OperandRight = op2
        });

    [Test]
    public async Task TestAdd()
    {
        _calculator.Setup(calculator => calculator.Add(1, 2)).Returns(Task.FromResult<double>(3));

        var result = await Calculate(MathOperator.Add, 1, 2 );

        Assert.That(result, Is.EqualTo("3"));
    }

    [Test]
    public async Task TestSubtract()
    {
        _calculator.Setup(calculator => calculator.Subtract(3, 2)).Returns(Task.FromResult<double>(1));

        var result = await Calculate(MathOperator.Subtract, 3, 2 );

        Assert.That(result, Is.EqualTo("1"));
    }

    [Test]
    public async Task TestMultiply()
    {
        _calculator.Setup(calculator => calculator.Multiply(7, 8)).Returns(Task.FromResult<double>(56));

        var result = await Calculate(MathOperator.Multiply, 7, 8 );

        Assert.That(result, Is.EqualTo("56"));
    }

    [Test]
    public async Task TestDivide()
    {
        _calculator.Setup(calculator => calculator.Divide(8, 4)).Returns(Task.FromResult<double>(2));

        var result = await Calculate(MathOperator.Divide, 8, 4 );

        Assert.That(result, Is.EqualTo("2"));
    }
}