using System.Globalization;

namespace GrpcClient;

internal enum MathOperator
{
    Add,
    Subtract,
    Multiply,
    Divide
}

/// <summary>
/// Fulfills calculation request from the command line, using a remote calculator service.
/// </summary>
/// <param name="client">Remote calculator client.</param>
/// <param name="culture">Culture to use for result formatting.</param>
internal class CliCalculator(IRemoteCalculatorClient client, CultureInfo? culture = null)
{
    private readonly CultureInfo _culture = culture ?? CultureInfo.CurrentCulture;

    public async Task<string> RunCalculation(CliOptions cliOptions)
    {
        var result = cliOptions.Operation switch
        {
            MathOperator.Add => await client.Add(cliOptions.OperandLeft, cliOptions.OperandRight),
            MathOperator.Subtract => await client.Subtract(cliOptions.OperandLeft, cliOptions.OperandRight),
            MathOperator.Multiply => await client.Multiply(cliOptions.OperandLeft, cliOptions.OperandRight),
            MathOperator.Divide => await client.Divide(cliOptions.OperandLeft, cliOptions.OperandRight),
        };

        return result.ToString(_culture);
    }
}