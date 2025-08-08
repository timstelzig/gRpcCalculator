using System.Globalization;

namespace GrpcClient;

internal class CalculatorCliClient
{
    private readonly ICalculatorClient _client;
    private readonly CultureInfo _culture;

    public CalculatorCliClient(ICalculatorClient client, CultureInfo? culture = null)
    {
        _client = client;
        _culture = culture ?? CultureInfo.CurrentCulture;
    }

    public async Task<string> RunCalculation(CliOptions cliOptions)
    {
        var result = cliOptions.Operation switch
        {
            CalculatorOperation.Add => await _client.Add(cliOptions.OperandLeft, cliOptions.OperandRight),
            CalculatorOperation.Subtract => await _client.Subtract(cliOptions.OperandLeft, cliOptions.OperandRight),
            CalculatorOperation.Multiply => await _client.Multiply(cliOptions.OperandLeft, cliOptions.OperandRight),
            CalculatorOperation.Divide => await _client.Divide(cliOptions.OperandLeft, cliOptions.OperandRight),
        };

        return result.ToString(_culture);
    }
}