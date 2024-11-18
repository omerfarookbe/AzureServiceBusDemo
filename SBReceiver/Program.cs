using Microsoft.Azure.ServiceBus;
using SBShared.Models;
using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace sbReceiver
{
    class program
    {
        const string sbConnection = "AZ SERVICE BUS CONNECTIONSTRING GOES HERE";
        const string queueName = "personqueue";
        static IQueueClient queueClient;

        static async Task Main(string[] args)
        {
            queueClient = new QueueClient(sbConnection, queueName);

            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };

            queueClient.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);

            Console.ReadLine();

            await queueClient.CloseAsync();
        }

        private static async Task ProcessMessagesAsync(Message message, CancellationToken token)
        {
            var jsonMessage = Encoding.UTF8.GetString(message.Body);
            Person person = JsonSerializer.Deserialize<Person>(jsonMessage);
            Console.WriteLine($"Person received: {person.FirstName} {person.LastName}");
            await queueClient.CompleteAsync(message.SystemProperties.LockToken);
        }

        private static Task ExceptionReceivedHandler(ExceptionReceivedEventArgs args)
        {
            Console.WriteLine($"Message handler exception {args.Exception}");
            return Task.CompletedTask;
        }
    }
}
