
using BenchmarkDotNet.Running;
using CustomerGrpc.Benchmarks.Benchmarks;

BenchmarkRunner.Run<JsonVsProtobufBenchmarks>();

