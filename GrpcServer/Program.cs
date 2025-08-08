using GrpcServer.GRpcInterface;
using GrpcServer.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddGrpc();
builder.Services.AddTransient<ICalculator, Calculator>();

var app = builder.Build();
app.MapGrpcService<GrpcCalculator>();
app.Run();