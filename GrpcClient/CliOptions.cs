using CommandLine;

namespace GrpcClient;


class CommonOptions
{
    [Option('h', "host", HelpText = "Hostname or IP address", Default = "localhost")]
    string Hostname { get; set; }
    [Option('p', "post", HelpText = "Port", Default = 8080)]
    short Port { get; set; }
}

[Verb("add", HelpText = "Add two numbers")]
class AddOptions : CommonOptions
{
    [Value(0, Required = true)]
    float OperandLeft { get; set; }
    [Value(1, Required = true)]
    float OperandRight { get; set; }
}

[Verb("subtract", HelpText = "Subtract two numbers")]
class SubtractOptions : CommonOptions
{
}