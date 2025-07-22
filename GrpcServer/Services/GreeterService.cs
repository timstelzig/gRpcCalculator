namespace GrpcServer.Services;

public interface IGreeterService
{
    public Task<string> SayHello(string name);
}

public class GreeterService : IGreeterService
{
    private readonly ILogger<GreeterService> _logger;

    public GreeterService(ILogger<GreeterService> logger)
    {
        _logger = logger;
    }

    public Task<string> SayHello(string name)
    {
        return Task.FromResult($"Hello {name}");
    }
}