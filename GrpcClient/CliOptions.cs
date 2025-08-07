using CommandLine;
using Shared;

namespace GrpcClient;

class CliOptions
{
    [Option('h', "host", HelpText = "Hostname or IP address", Default = "localhost")]
    string Hostname { get; set; }

    [Option('p', "post", HelpText = "Port", Default = 5299)]
    short Port { get; set; }

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