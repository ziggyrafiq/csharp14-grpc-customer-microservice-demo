
using CustomerGrpc;
using Grpc.Core;
using Grpc.Net.Client;

Console.WriteLine("Hello, from Ziggy Rafiq!");


Console.WriteLine("Customer gRPC demo client starting...");

using var channel = GrpcChannel.ForAddress("https://localhost:7118");
var client = new CustomerService.CustomerServiceClient(channel);

var created = await client.CreateCustomerAsync(new CreateCustomerRequest
{
    FullName = "Ziggy Rafiq",
    Email = "ziggy@example.com"
});

Console.WriteLine($"Created: {created.CustomerId} - {created.FullName}");

var fetched = await client.GetCustomerByIdAsync(new GetCustomerByIdRequest
{
    CustomerId = created.CustomerId
});

Console.WriteLine($"Fetched: {fetched.CustomerId} - {fetched.Email}");

Console.WriteLine("\nStreaming customers...");

using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
var stream = client.StreamCustomers(new StreamCustomersRequest { DelayMs = 300 }, cancellationToken: cts.Token);

await foreach (var item in stream.ResponseStream.ReadAllAsync(cts.Token))
{
    Console.WriteLine($"→ {item.CustomerId} | {item.FullName} | {item.Email}");
}

Console.ReadLine();