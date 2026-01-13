# C# 14 gRPC Customer Microservice Demo (.NET 10)
csharp14-grpc-customer-microservice-demo shows how to use gRPC + Protobuf instead of REST for internal microservice communication in C# 14 and .NET 10.

It includes:
- a **Customer gRPC Server**
- a **Customer gRPC Client**
- **BenchmarkDotNet** benchmarks comparing **JSON vs Protobuf**

 **[Article published on C# Corner click here to read this article](https://www.c-sharpcorner.com/article/using-c-sharp-14-with-grpc-instead-of-rest-build-a-customer-microservice-with-bench/)**

## Usage
The demo can be run by following these steps:

1. Clone this repository.
2. Navigate to the repo directory.
3. Run the server and client.
   
### Run the gRPC Server
```bash
dotnet run --project .\CustomerGrpc.Server
```

### Run the Client App
Open a new terminal and run:

```bash
dotnet run --project .\CustomerGrpc.Client
```

## BenchmarkDotNet: JSON vs Protobuf
Serialization costs are a significant component of latency and CPU usage in internal services. This repo includes BenchmarkDotNet to measure payload performance (instead of guessing).

### Run Benchmarks
Benchmarks should be run in **Release mode**:

```bash
dotnet run -c Release --project .\CustomerGrpc.Benchmarks\CustomerGrpc.Benchmarks.csproj
```

The benchmark compares:
- JSON payload size and serialization cost
- Protobuf payload size and serialization cost
- allocations / GC pressure

## Projects Included

- `CustomerGrpc.Server` → gRPC server (.NET 10)
- `CustomerGrpc.Client` → console client for testing calls
- `CustomerGrpc.Benchmarks` → BenchmarkDotNet project (JSON vs Protobuf)



## About The Author Ziggy Rafiq

Ziggy Rafiq works as a Technical Lead Developer and is a prominent public figure in the tech industry and developer community. With over 20 years of experience as a Full-Stack Designer, Developer, Tester, DevSecOps, Technical Architect,
Software Project Manager, and expert in Agile Management Best Practices and Standards, Ziggy is widely respected for his knowledge and skills. He is also an author, regularly contributing articles
to [C# Corner](https://www.c-sharpcorner.com/members/ziggy-rafiq), and [Geek Coding](https://geekcodinghub.com/members/ziggy-rafiq.html) to
share his expertise and wisdom with a global audience. Ziggy also has a YouTube Channel where he creates content based on his experience to help and inspire other developers, designers, testers, project managers,
technical architects, and Agile Scrum masters.

- **Technical Lead Developer, Mentor and Trainer**
- **[C# Corner (MVP 🏅, VIP⭐️, Public Speaker🎤)](https://www.c-sharpcorner.com/members/ziggy-rafiq)**
- **[Geek Coding Writter](https://geekcodinghub.com/members/ziggy-rafiq.html)**
- **[Technology Manager Writter](https://technologymanagerhub.com/members/ziggy-rafiq.html)**
- Link to [**Ziggy Rafiq Blog**](https://blog.ziggyrafiq.com)
- Link to [**Ziggy Rafiq Website**](https://ziggyrafiq.com)

* [**Please remember to subscribe to My YouTube channel**](https://www.youtube.com/)
* [**Please remember to follow me on LinkedIn**](https://www.linkedin.com/in/ziggyrafiq/)
* [**Please remember to follow me on Twitter/X**](https://twitter.com/ziggyrafiq)
* [**Please remember to follow me on Instagram**](https://www.instagram.com/ziggyrafiq/)
* [**Please remember to follow me on Facebook**](https://www.facebook.com/ziggyrafiq)

## Contributing
Any improvements or corrections should be submitted as a pull request or opened in an issue.

## License
The LICENSE file contains details about how this project is licensed under the MIT License.
