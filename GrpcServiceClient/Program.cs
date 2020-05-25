using Grpc.Net.Client;
using GrpcService;
using System;
using System.Net.Http;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace GrpcServiceClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            using var channel = GrpcChannel.ForAddress("http://localhost:5000");
            var client = new Greeter.GreeterClient(channel);

            Console.WriteLine("Type you name, please:");
            var name = Console.ReadLine();

            var reply = await client.SayHelloAsync(
                              new HelloRequest { Name = name });
            Console.WriteLine("Greeting: " + reply.Message);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
