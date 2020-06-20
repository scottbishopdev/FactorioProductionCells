using System;
using System.Text;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using FactorioProductionCells.Application.Common.Interfaces;

namespace FactorioProductionCells.Infrastructure.Messaging
{
    public class RabbitMqAdapter : IMessageQueue, IDisposable
    {
        private const String RabbitMQHostName = "RabbitMQ";
        
        private readonly ILogger<RabbitMqAdapter> _logger;
        private IConnection _connection;
        private IModel _addModChannel;
        private IModel _updateModChannel;
        
        public RabbitMqAdapter(
            ILogger<RabbitMqAdapter> logger)
        {
            _logger = logger;

            _logger.LogInformation($"RabbitMQ UserName:    {Environment.GetEnvironmentVariable("RABBITMQ_DEFAULT_USER")}");
            _logger.LogInformation($"RabbitMQ Password:    {Environment.GetEnvironmentVariable("RABBITMQ_DEFAULT_PASS")}");
            _logger.LogInformation($"RabbitMQ HostName:    {RabbitMqAdapter.RabbitMQHostName}");
            _logger.LogInformation($"RabbitMQ Port:        {Convert.ToInt32(Environment.GetEnvironmentVariable("RABBITMQ_PORT"))}");

            var factory = new ConnectionFactory
            {
                UserName = Environment.GetEnvironmentVariable("RABBITMQ_DEFAULT_USER"),
                Password = Environment.GetEnvironmentVariable("RABBITMQ_DEFAULT_PASS"),
                HostName = RabbitMqAdapter.RabbitMQHostName,
                Port = Convert.ToInt32(Environment.GetEnvironmentVariable("RABBITMQ_PORT")),
            };

            _connection = factory.CreateConnection();
            _logger.LogInformation($"Obtained a connection to RabbitMQ.");

            _addModChannel = _connection.CreateModel();
            _logger.LogInformation($"Obtained a model for the AddMod channel.");
            _addModChannel.QueueDeclare(
                queue: "AddMod",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);
            _logger.LogInformation($"Declared the queue for the AddMod channel.");

            _updateModChannel = _connection.CreateModel();
            _logger.LogInformation($"Obtained a model for the UpdateMod channel.");
            _updateModChannel.QueueDeclare(
                queue: "UpdateMod",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);
            _logger.LogInformation($"Declared the queue for the UpdateMod channel.");
        }

        // TODO: Figure out if there's a way that I can allow callers to directly reference the channels defined on this adapter, instead of having to manually 
        // provide the name of the channel that they want to send the message to.
        public void SendMessage(String channel, String message)
        {
            _logger.LogInformation($"Attempting to publish the message \"{message}\" to the channel \"{channel}\".");
            var body = Encoding.UTF8.GetBytes(message);

            if(channel == "AddMod")
            {
                _addModChannel.BasicPublish(
                    exchange: "",
                    routingKey: "AddMod",
                    basicProperties: null,
                    body: body);
            }
            else if (channel == "UpdateMod")
            {
                _updateModChannel.BasicPublish(
                    exchange: "",
                    routingKey: "UpdateMod",
                    basicProperties: null,
                    body: body);
            }
        }

        public void Dispose()
        {
            _addModChannel.Dispose();
            _updateModChannel.Dispose();
            _connection.Dispose();
        }
    }
}
