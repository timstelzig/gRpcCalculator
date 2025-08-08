namespace GrpcServer.Services;

public interface ICalculator
{
    public double Add(double leftSummand, double rightSummand);
    public double Subtract(double minuend, double subtrahend);
    public double Multiply(double leftFactor, double rightFactor);
    public double Divide(double dividend, double divisor);
}

public class CalculatorService : ICalculator
{
    private readonly ILogger<CalculatorService> _logger;

    public CalculatorService(ILogger<CalculatorService> logger)
    {
        _logger = logger;
    }

    public double Add(double leftSummand, double rightSummand) => leftSummand + rightSummand;

    public double Subtract(double minuend, double subtrahend) => minuend - subtrahend;

    public double Multiply(double leftFactor, double rightFactor) => leftFactor * rightFactor;

    public double Divide(double dividend, double divisor)
    {
        if (divisor == 0)
            throw new DivideByZeroException();
        return dividend / divisor;
    }
}