using BenchmarkDotNet.Attributes;
using CustomerGrpc.Benchmarks.Models;
using CustomerGrpc.Benchmarks.Protos;
using Google.Protobuf;
using System.Text.Json;

namespace CustomerGrpc.Benchmarks.Benchmarks;

[MemoryDiagnoser]
public class JsonVsProtobufBenchmarks
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    private CustomerJsonDto _jsonDto = default!;
    private CustomerProto _proto = default!;

    private byte[] _jsonBytes = default!;
    private byte[] _protoBytes = default!;

    [GlobalSetup]
    public void Setup()
    {
        _jsonDto = new CustomerJsonDto(
            CustomerId: Guid.NewGuid(),
            FullName: "Ziggy Rafiq",
            Email: "ziggy@example.com",
            CreatedUtc: DateTime.UtcNow);

        _proto = new CustomerProto
        {
            CustomerId = _jsonDto.CustomerId.ToString(),
            FullName = _jsonDto.FullName,
            Email = _jsonDto.Email,
            CreatedUtc = _jsonDto.CreatedUtc.ToString("O")
        };

        // Pre-serialized payloads for deserialize benchmarks
        _jsonBytes = JsonSerializer.SerializeToUtf8Bytes(_jsonDto, JsonOptions);
        _protoBytes = _proto.ToByteArray();
    }

    // -----------------------------
    // Size comparisons (no timing)
    // -----------------------------

    [Benchmark(Description = "JSON Size (bytes)")]
    public int Json_Size() => _jsonBytes.Length;

    [Benchmark(Description = "Protobuf Size (bytes)")]
    public int Protobuf_Size() => _protoBytes.Length;

    // -----------------------------
    // Serialization
    // -----------------------------

    [Benchmark(Baseline = true, Description = "JSON Serialize")]
    public byte[] Json_Serialize()
        => JsonSerializer.SerializeToUtf8Bytes(_jsonDto, JsonOptions);

    [Benchmark(Description = "Protobuf Serialize")]
    public byte[] Protobuf_Serialize()
        => _proto.ToByteArray();

    // -----------------------------
    // Deserialization
    // -----------------------------

    [Benchmark(Description = "JSON Deserialize")]
    public CustomerJsonDto? Json_Deserialize()
        => JsonSerializer.Deserialize<CustomerJsonDto>(_jsonBytes, JsonOptions);

    [Benchmark(Description = "Protobuf Deserialize")]
    public CustomerProto Protobuf_Deserialize()
        => CustomerProto.Parser.ParseFrom(_protoBytes);
}
