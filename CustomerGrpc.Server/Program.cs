using CustomerGrpc.Server.Data;
using CustomerGrpc.Server.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.AddSingleton<ICustomerRepository, CustomerRepository>();
builder.Services.AddLogging(x => x.AddConsole());

var app = builder.Build();

app.MapGrpcService<CustomerGrpcService>();
app.MapGet("/", () => "Customer gRPC service running.");

app.Run();
