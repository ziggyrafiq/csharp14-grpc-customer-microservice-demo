using CustomerGrpc.Server.Data;
using Grpc.Core;

namespace CustomerGrpc.Server.Services;

public sealed class CustomerGrpcService(
    ILogger<CustomerGrpcService> logger,
    ICustomerRepository repository)
    : CustomerGrpc.CustomerService.CustomerServiceBase
{
    public override Task<CustomerResponse> CreateCustomer(
        CreateCustomerRequest request,
        ServerCallContext context)
    {
        if (string.IsNullOrWhiteSpace(request.FullName))
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Full name is required."));

        if (string.IsNullOrWhiteSpace(request.Email) || !request.Email.Contains('@'))
            throw new RpcException(new Status(StatusCode.InvalidArgument, "A valid email is required."));

        var customer = repository.Add(request.FullName.Trim(), request.Email.Trim());

        logger.LogInformation("Customer created: {CustomerId} ({Email})", customer.Id, customer.Email);

        return Task.FromResult(Map(customer));
    }

    public override Task<CustomerResponse> GetCustomerById(
        GetCustomerByIdRequest request,
        ServerCallContext context)
    {
        if (!Guid.TryParse(request.CustomerId, out var id))
            throw new RpcException(new Status(StatusCode.InvalidArgument, "CustomerId must be a valid GUID."));

        var customer = repository.Get(id);

        if (customer is null)
            throw new RpcException(new Status(StatusCode.NotFound, $"Customer '{id}' not found."));

        return Task.FromResult(Map(customer));
    }

    public override async Task StreamCustomers(
        StreamCustomersRequest request,
        IServerStreamWriter<CustomerResponse> responseStream,
        ServerCallContext context)
    {
        var delayMs = request.DelayMs <= 0 ? 250 : request.DelayMs;

        foreach (var customer in repository.GetAll())
        {
            if (context.CancellationToken.IsCancellationRequested)
                break;

            await responseStream.WriteAsync(Map(customer));
            await Task.Delay(delayMs, context.CancellationToken);
        }
    }

    private static CustomerResponse Map(Customer customer) => new()
    {
        CustomerId = customer.Id.ToString(),
        FullName = customer.FullName,
        Email = customer.Email,
        CreatedUtc = customer.CreatedUtc.ToString("O")
    };
}
