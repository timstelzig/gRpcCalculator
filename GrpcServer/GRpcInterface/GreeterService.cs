using Grpc.Core;
using GrpcGreeterService;
using GrpcServer.Services;

namespace GrpcServer.GRpcInterface;

public class GrpcGreeterService : Greeter.GreeterBase
{
    private readonly ILogger<GreeterService> _logger;
    private readonly IGreeterService _greeterService;

    public GrpcGreeterService(ILogger<GreeterService> logger, IGreeterService greeterService)
    {
        _logger = logger;
        _greeterService = greeterService;
    }

    public override async Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
        return new HelloReply
        {
            Message = await _greeterService.SayHello(request.Name)
        };
    }
}