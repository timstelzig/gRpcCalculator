using System.Globalization;

namespace GrpcClient;

internal enum MathOperator
{
    Add,
    Subtract,
    Multiply,
    Divide
}

internal class CalculatorCliClient
{
    private readonly IRemoteCalculatorClient _client;
    private readonly CultureInfo _culture;

    public CalculatorCliClient(IRemoteCalculatorClient client, CultureInfo? culture = null)
    {
        _client = client;
        _culture = culture ?? CultureInfo.CurrentCulture;
    }

    public async Task<string> RunCalculation(CliOptions cliOptions)
    {
        var result = cliOptions.Operation switch
        {
            MathOperator.Add => await _client.Add(cliOptions.OperandLeft, cliOptions.OperandRight),
            MathOperator.Subtract => await _client.Subtract(cliOptions.OperandLeft, cliOptions.OperandRight),
            MathOperator.Multiply => await _client.Multiply(cliOptions.OperandLeft, cliOptions.OperandRight),
            MathOperator.Divide => await _client.Divide(cliOptions.OperandLeft, cliOptions.OperandRight),
        };

        return result.ToString(_culture);
    }
}