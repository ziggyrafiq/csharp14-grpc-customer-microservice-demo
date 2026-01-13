namespace CustomerGrpc.Server.Data;

public sealed record Customer(
    Guid Id,
    string FullName,
    string Email,
    DateTime CreatedUtc);

public interface ICustomerRepository
{
    Customer Add(string fullName, string email);
    Customer? Get(Guid id);
    IReadOnlyList<Customer> GetAll();
}

public sealed class CustomerRepository : ICustomerRepository
{
    private readonly List<Customer> _customers = [];

    public Customer Add(string fullName, string email)
    {
        var customer = new Customer(Guid.NewGuid(), fullName, email, DateTime.UtcNow);
        _customers.Add(customer);
        return customer;
    }

    public Customer? Get(Guid id) => _customers.FirstOrDefault(x => x.Id == id);

    public IReadOnlyList<Customer> GetAll() => _customers.AsReadOnly();
}
