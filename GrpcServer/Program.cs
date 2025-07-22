using GrpcServer.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddTransient<IGreeterService, GreeterService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GrpcServer.GRpcInterface.GrpcGreeterService>();

app.Run();