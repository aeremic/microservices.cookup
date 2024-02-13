using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Queuing.Common;
using Queuing.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Queuing;

public class
    QueueConsumerHandler<TMessageConsumer, TQueueMessage> : IQueueConsumerHandler<TMessageConsumer, TQueueMessage>
    where TMessageConsumer : IQueueConsumer<TQueueMessage> where TQueueMessage : class, IQueueMessage
{
    private readonly string _queueName;
    private readonly string _consumerName;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<QueueConsumerHandler<TMessageConsumer, TQueueMessage>> _logger;

    private IChannel _consumerRegistrationChannel;
    private string _consumerSubscriptionTag;

    public QueueConsumerHandler(IServiceProvider serviceProvider,
        ILogger<QueueConsumerHandler<TMessageConsumer, TQueueMessage>> logger)
    {
        _queueName = typeof(TQueueMessage).Name;
        _consumerName = typeof(TMessageConsumer).Name;

        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public void RegisterQueueConsumer()
    {
        _logger.LogInformation($"Registering {_consumerName} as a consumer for {_queueName} queue.");

        var scope = _serviceProvider.CreateScope();
        _consumerRegistrationChannel = scope.ServiceProvider
            .GetRequiredService<IQueueChannelProvider<TQueueMessage>>()
            .GetChannel();

        var consumer = new AsyncEventingBasicConsumer(_consumerRegistrationChannel);
        consumer.Received += HandleMessage;
        try
        {
            _consumerSubscriptionTag = _consumerRegistrationChannel.BasicConsume(_queueName, false, consumer);
        }
        catch (Exception ex)
        {
            var errorMessage = $"BasicConsume failed for {_queueName} queue. {ex.Message}";
            _logger.LogError(errorMessage);

            throw new QueuingException(errorMessage);
        }
    }

    public void CancelQueueConsumer()
    {
        _logger.LogInformation($"Cancelling {_consumerName} registration for {_queueName} queue.");

        try
        {
            _consumerRegistrationChannel.BasicCancel(_consumerSubscriptionTag);
        }
        catch (Exception ex)
        {
            var errorMessage = $"BasicCancel failed for {_queueName} queue. {ex.Message}";
            _logger.LogError(errorMessage);

            throw new QueuingException(errorMessage);
        }
    }

    private async Task HandleMessage(object channel, BasicDeliverEventArgs eventArgs)
    {
        _logger.LogInformation($"Received message on {_queueName}.");

        var consumingChannel = ((AsyncEventingBasicConsumer)channel).Channel;
        var consumerScope = _serviceProvider.CreateScope();

        IChannel? producingChannel = null; // Producing channel for commiting transactions.
        try
        {
            producingChannel = consumerScope.ServiceProvider.GetRequiredService<IChannelProvider>().GetChannel();

            var message = DeserializeMessage(eventArgs.Body.ToArray());
            _logger.LogInformation($"Message with identifier {message!.MessageId} received.");

            producingChannel.TxSelect(); // Start a transaction.

            var consumerInstance = consumerScope.ServiceProvider.GetRequiredService<TMessageConsumer>();
            await consumerInstance.ConsumeAsync(message); // Trigger consumer to start processing message.

            // Check channels before commiting.
            if (producingChannel.IsClosed || consumingChannel.IsClosed)
            {
                throw new QueuingException("A channel is closed during processing transaction.");
            }

            producingChannel.TxCommit(); // Transaction commit.

            consumingChannel.BasicAck(eventArgs.DeliveryTag, false); // Sends ack as message is successfully handled.
        }
        catch (Exception ex)
        {
            var errorMessage = $"Cannot handle consumption of {_queueName} queue by {_consumerName}. {ex.Message}";
            _logger.LogError(errorMessage);

            RejectMessage(eventArgs.DeliveryTag, consumingChannel, producingChannel); // Rollback transaction.
        }
        finally
        {
            consumerScope.Dispose();
        }
    }

    private void RejectMessage(ulong deliveryTag, IChannel consumingChannel, IChannel? producingChannel)
    {
        try
        {
            if (producingChannel is not null)
            {
                producingChannel.TxRollback();
                _logger.LogInformation("Transaction is rollback.");
            }

            consumingChannel.BasicReject(deliveryTag, false);
            _logger.LogInformation("Rejected queue message.");
        }
        catch (Exception ex)
        {
            var errorMessage =
                $"BasicReject failed for consumption of {_queueName} queue by {_consumerName}. {ex.Message}";
            _logger.LogCritical(ex, errorMessage);
        }
    }

    private static TQueueMessage? DeserializeMessage(byte[] message)
    {
        var content = Encoding.UTF8.GetString(message);

        return JsonConvert.DeserializeObject<TQueueMessage>(content);
    }
}