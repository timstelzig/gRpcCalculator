namespace GrpcServer.Services;

/// <summary>
/// Basic mathematical operations on double.
/// </summary>
public interface ICalculator
{
    public double Add(double leftSummand, double rightSummand);
    public double Subtract(double minuend, double subtrahend);
    public double Multiply(double leftFactor, double rightFactor);
    public double Divide(double dividend, double divisor);
}

/// <summary>
/// Calculator for basic double operations.
///
/// Using default IEEE-754 operations, except for <see cref="Divide"/>.
/// </summary>
public class Calculator : ICalculator
{
    public double Add(double leftSummand, double rightSummand) => leftSummand + rightSummand;

    public double Subtract(double minuend, double subtrahend) => minuend - subtrahend;

    public double Multiply(double leftFactor, double rightFactor) => leftFactor * rightFactor;

    /// <summary>
    /// Division disallowing dividing by zero.
    /// </summary>
    /// <exception cref="DivideByZeroException">Thrown if the divisor is 0.</exception>
    public double Divide(double dividend, double divisor)
    {
        // By default, dividing a double by 0 returns Infinity. That is not what users expect from a calculator.
        if (divisor == 0)
            throw new DivideByZeroException();
        return dividend / divisor;
    }
}