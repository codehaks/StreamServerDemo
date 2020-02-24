using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyClient
{
    class Program
    {
        private static HubConnection connection;

        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            connection = new HubConnectionBuilder()
              .WithUrl("http://localhost:5000/streamHub")
              .ConfigureLogging(logging =>
              {
                  logging.AddDebug();
                  logging.SetMinimumLevel(LogLevel.Debug);
              })
              .Build();

            connection.Closed += async (error) =>
            {
                await Task.Delay(1000);
                await connection.StartAsync();
            };

            await connection.StartAsync();

            var cancellationTokenSource = new CancellationTokenSource();

            var stream = connection.StreamAsync<string>("SendDataRow", cancellationTokenSource.Token);

            await foreach (var trackingCode in stream)
            {
                Console.WriteLine($"{trackingCode}");
            }

            Console.WriteLine("Streaming completed");          

            Console.ReadLine();
        }
    }
}
