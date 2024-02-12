using System.Globalization;
using System.Text;
using System.Text.Json.Nodes;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Queuing.Common;
using Queuing.Interfaces;
using RabbitMQ.Client;

namespace Queuing.Implementation;

public class QueueProducer<TQueueMessage> : IQueueProducer<TQueueMessage> where TQueueMessage : IQueueMessage
{
    private readonly IChannel _channel;
    private readonly string _queueName;
    private readonly ILogger<QueueProducer<TQueueMessage>> _logger;

    public QueueProducer(IQueueChannelProvider<TQueueMessage> channelProvider,
        ILogger<QueueProducer<TQueueMessage>> logger)
    {
        _channel = channelProvider.GetChannel();
        _queueName = typeof(TQueueMessage).Name;
        _logger = logger;
    }

    public void PublishMessage(TQueueMessage message)
    {
        if (Equals(message, default(TQueueMessage)))
        {
            throw new ArgumentNullException(nameof(message));
        }

        if (message.TimeToLive.Ticks <= 0)
        {
            throw new QueuingException($"{nameof(message.TimeToLive)} cannot be less or equal than zero.");
        }

        message.MessageId = new Guid();

        try
        {
            _logger.LogInformation($"Publishing message to ${_queueName} queue.");

            var serializedMessage = SerializeMessage(message);
            _channel.BasicPublish(_queueName, _queueName, new BasicProperties
            {
                Persistent = true,
                Type = _queueName,
                Expiration = message.TimeToLive.TotalMicroseconds.ToString(CultureInfo.InvariantCulture)
            }, serializedMessage);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Cannot publish message to ${_queueName} queue.");
        }
    }

    private static byte[] SerializeMessage(TQueueMessage message)
    {
        var content = JsonConvert.SerializeObject(message);

        return Encoding.UTF8.GetBytes(content);
    }
}