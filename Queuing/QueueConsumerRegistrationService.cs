using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Queuing.Interfaces;

namespace Queuing;

internal class QueueConsumerRegistrationService<TMessageConsumer, TQueueMessage> : IHostedService
    where TMessageConsumer : IQueueConsumer<TQueueMessage> where TQueueMessage : class, IQueueMessage
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<QueueConsumerRegistrationService<TMessageConsumer, TQueueMessage>> _logger;

    private IServiceScope? _scope;
    private IQueueConsumerHandler<TMessageConsumer, TQueueMessage>? _consumerHandler;

    public QueueConsumerRegistrationService(IServiceProvider serviceProvider,
        ILogger<QueueConsumerRegistrationService<TMessageConsumer, TQueueMessage>> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            $"Registering {typeof(TMessageConsumer).Name} as a consumer for {typeof(TQueueMessage).Name} queue.");

        // Every message for the QueueConsumer will have their own incoming channel,
        // since every registration of IQueueConsumerHandler will have it's own scope.
        _scope = _serviceProvider.CreateScope();
        _consumerHandler = _scope.ServiceProvider
            .GetRequiredService<IQueueConsumerHandler<TMessageConsumer, TQueueMessage>>
                ();
        _consumerHandler.RegisterQueueConsumer();

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            $"Stopping {nameof(QueueConsumerHandler<TMessageConsumer, TQueueMessage>)}: " +
            $"Cancelling {typeof(TMessageConsumer).Name} as a consumer for {typeof(TQueueMessage).Name} queue.");
        
        _consumerHandler!.CancelQueueConsumer();
        _scope!.Dispose();

        return Task.CompletedTask;
    }
}