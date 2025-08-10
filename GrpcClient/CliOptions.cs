using CommandLine;

namespace GrpcClient;

internal record CliOptions
{
    [Option('h', "host", HelpText = "gRPC Server Hostname or IP address")]
    public string Hostname { get; set; } = "localhost";

    [Option('p', "post", HelpText = "gRPC Server Port")]
    public short Port { get; set; } = 5299;

    [Value(0, Required = true, HelpText = "The operation to perform. One of Add, Subtract, Multiply, Divide.")]
    public MathOperator Operation { get; set; }

    [Value(1, Required = true, HelpText = "The first operand.")]
    public double OperandLeft { get; set; }

    [Value(2, Required = true, HelpText = "The second operand.")]
    public double OperandRight { get; set; }

    public ConnectionOptions GetConnectionOptions() => new(Hostname, Port);
}

/// <summary>
/// Provides a parser for <see cref="CliOptions"/>.
/// </summary>
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