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
        var result = await _client.PerformCalculation(cliOptions.Operation, cliOptions.OperandLeft, cliOptions.OperandRight);
        return result.ToString(_culture);
    }
}