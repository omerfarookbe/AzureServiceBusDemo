using Microsoft.Azure.ServiceBus;
using SBShared.Models;
using System.Text;
using System.Text.Json;

namespace SBSender.Services
{
    public class QueueService : IQueueService
    {
        private readonly IConfiguration _configuration;

        public QueueService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendMessageAsync<T>(T servicebusMessage, string queueName)
        {

            var queueClient = new QueueClient(_configuration.GetConnectionString("AZ_ServiceBus"), queueName);

            string messageBody = JsonSerializer.Serialize(servicebusMessage);
            var messageByte = new Message(Encoding.UTF8.GetBytes(messageBody));

            await queueClient.SendAsync(messageByte);

            try
            {

            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}
