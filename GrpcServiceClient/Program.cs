using Grpc.Net.Client;
using GrpcService;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GrpcServiceClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var httpClient = new HttpClient(httpClientHandler);

            // The port number(5001) must match the port of the gRPC server.
            using var channel = GrpcChannel.ForAddress("https://localhost:5001"
                , new GrpcChannelOptions { HttpClient = httpClient }
                );
            var client = new Greeter.GreeterClient(channel);

            var reply = await client.SayHelloAsync(
                              new HelloRequest { Name = "GreeterClient" });
            Console.WriteLine("Greeting: " + reply.Message);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
