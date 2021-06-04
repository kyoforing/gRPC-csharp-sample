using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Net.Client;
using GrpcNoteClient;

namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // This switch must be set before creating the GrpcChannel/HttpClient.
            AppContext.SetSwitch(
                "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            // The port number(5001) must match the port of the gRPC server.
            using var channel = GrpcChannel.ForAddress("http://127.0.0.1:50051");
            var client = new NoteService.NoteServiceClient(channel);
            var reply = await client.ListAsync(new Empty {});

            Console.WriteLine("Notes: " + reply.Notes);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
