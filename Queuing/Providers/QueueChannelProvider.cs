using Queuing.Common;
using Queuing.Interfaces;
using RabbitMQ.Client;

namespace Queuing.Providers;

internal class QueueChannelProvider<TQueueMessage> : IQueueChannelProvider<TQueueMessage>
    where TQueueMessage : IQueueMessage
{
    private IModel? _channel;

    private readonly IChannelProvider _channelProvider;
    private readonly string _queueName;

    public QueueChannelProvider(IChannelProvider channelProvider)
    {
        _channelProvider = channelProvider;
        _queueName = typeof(TQueueMessage).Name;
    }

    public IModel GetChannel()
    {
        _channel = _channelProvider.GetChannel();
        DeclareQueues();

        return _channel;
    }

    private void DeclareQueues()
    {
        if (_channel is null)
        {
            return;
        }

        var deadLetterQueueName = $"{_queueName}{QueueingConstants.DeadLetterAddition}";

        _channel.ExchangeDeclare(deadLetterQueueName, ExchangeType.Direct); // Declare mailbox.
        _channel.QueueDeclare(deadLetterQueueName, true, false, false,
            new Dictionary<string, object>()
            {
                { "x-queue-type", "quorum" },
                { "overflow", "reject-publish" }
            });
        _channel.QueueBind(deadLetterQueueName, deadLetterQueueName, deadLetterQueueName, null);

        _channel.ExchangeDeclare(_queueName, ExchangeType.Direct); // Declare mailbox.
        _channel.QueueDeclare(_queueName, true, false, false,
            new Dictionary<string, object>()
            {
                { "x-dead-letter-exchange", deadLetterQueueName },
                { "x-dead-letter-routing-key", deadLetterQueueName },
                { "x-queue-type", "quorum" },
                { "x-dead-letter-strategy", "at-least-once" },
                { "overflow", "reject-publish" }
            });
        _channel.QueueBind(_queueName, _queueName, _queueName, null);
    }
}