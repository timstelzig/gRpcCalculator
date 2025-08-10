using GrpcServer.GRpcInterface;
using GrpcServer.Services;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

// gRPC requires Http2
builder.WebHost.ConfigureKestrel(options =>
    options.ConfigureEndpointDefaults(endpointOptions => endpointOptions.Protocols = HttpProtocols.Http2));

builder.Services.AddGrpc();
builder.Services.AddTransient<ICalculator, Calculator>();

var app = builder.Build();
app.MapGrpcService<CalculatorGrpcServer>();
app.Run();