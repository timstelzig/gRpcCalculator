using CommandLine;

namespace GrpcClient;

internal record CliOptions
{
    [Option('h', "host", HelpText = "Hostname or IP address")]
    public string Hostname { get; set; } = "localhost";

    [Option('p', "post", HelpText = "Port")]
    public short Port { get; set; } = 5299;

    [Value(0, Required = true, HelpText = "The operation to perform. One of Add, Subtract, Multiply, Divide.")]
    public CalculatorOperation Operation { get; set; }

    [Value(1, Required = true, HelpText = "The first operand.")]
    public float OperandLeft { get; set; }

    [Value(2, Required = true, HelpText = "The second operand.")]
    public float OperandRight { get; set; }

    public ConnectionOptions GetConnectionOptions() => new(Hostname, Port);
}

internal static class CliParser
{
    public static ParserResult<CliOptions> ParseArgs(string[] args)
    {
        var parser = new Parser(settings =>
        {
            settings.CaseInsensitiveEnumValues = true;
            settings.HelpWriter = Console.Error;
        });
        return parser.ParseArguments<CliOptions>(args);
    }
}