using GrpcGreeterService;

namespace GrpcClient;

public class CalculatorGrpcClient : ICalculatorClient
{
    private readonly Greeter.GreeterClient _client;

    public CalculatorGrpcClient(Greeter.GreeterClient client)
    {
        _client = client;
    }
    
    public async Task<string> SayHelloAsync(string name)
    {
        var response = await _client.SayHelloAsync(new HelloRequest { Name = name });
        return response.Message;
    }
}

public interface ICalculatorClient
{
    Task<string> SayHelloAsync(string name);
}