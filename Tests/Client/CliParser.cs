using CommandLine;
using GrpcClient;

namespace Tests.Client;

/// <summary>
/// Test that command line parameters are parsed correctly.
/// </summary>
public class CliParserTests
{
    [Test]
    public void TestParseBasicOperation()
    {
        var parsed = CliParser.ParseArgs(["add", "1", "2"]);
        Assert.That(parsed, Is.TypeOf<Parsed<CliOptions>>());
        Assert.That(parsed.Value,
            Is.EqualTo(new CliOptions { OperandLeft = 1, OperandRight = 2, Operation = MathOperator.Add }));
    }

    [Test]
    public void TestParseHostnameAndPort()
    {
        var parsed = CliParser.ParseArgs(["-h", "192.168.0.123", "-p", "1234", "add", "1", "2"]);
        Assert.That(parsed, Is.TypeOf<Parsed<CliOptions>>());
        Assert.That(parsed.Value,
            Is.EqualTo(new CliOptions
            {
                OperandLeft = 1, OperandRight = 2, Operation = MathOperator.Add, Port = 1234,
                Hostname = "192.168.0.123"
            }));
    }
}