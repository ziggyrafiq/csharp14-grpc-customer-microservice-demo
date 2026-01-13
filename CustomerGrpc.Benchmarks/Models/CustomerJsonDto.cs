namespace CustomerGrpc.Benchmarks.Models;

public sealed record CustomerJsonDto(
    Guid CustomerId,
    string FullName,
    string Email,
    DateTime CreatedUtc
);
