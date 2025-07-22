using Shared;

namespace GrpcServer.Services;

public interface ICalculator
{
    public double PerformCalculation(CalculatorOperation operation, double left, double right);
}

public class CalculatorService : ICalculator
{
    private readonly ILogger<CalculatorService> _logger;

    public CalculatorService(ILogger<CalculatorService> logger)
    {
        _logger = logger;
    }

    public double PerformCalculation(CalculatorOperation operation, double left, double right)
    {
        return operation switch
        {
            CalculatorOperation.Add => left + right,
            CalculatorOperation.Subtract => left - right,
            CalculatorOperation.Multiply => left * right,
            CalculatorOperation.Divide => left / right,
        };
    }
}