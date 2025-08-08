using GrpcServer.GRpcInterface;
using GrpcServer.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddTransient<ICalculator, Calculator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GrpcCalculator>();

app.Run();