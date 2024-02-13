using Microsoft.Extensions.DependencyInjection;
using Queuing.Implementation;
using Queuing.Interfaces;
using Queuing.Providers;
using RabbitMQ.Client;

namespace Queuing.Extensions;

public static class QueueingStartupExtension
{
    public static void AddQueueing(this IServiceCollection services, QueueingConfigurationSettings settings)
    {
        services.AddSingleton(settings);

        // Establish connection as a singleton.
        services.AddSingleton<IAsyncConnectionFactory>(_ =>
        {
            var factory = new ConnectionFactory
            {
                UserName = settings.Username,
                Password = settings.Password,
                HostName = settings.Hostname,
                Port = settings.Port.GetValueOrDefault(),

                DispatchConsumersAsync = true,
                AutomaticRecoveryEnabled = true,

                ConsumerDispatchConcurrency = settings.ConsumerConcurrency.GetValueOrDefault()
            };

            return factory;
        });

        // Register connection provider.
        services.AddSingleton<IConnectionProvider, ConnectionProvider>();

        // Register channel provider.
        services.AddScoped<IChannelProvider, ChannelProvider>();
        services.AddScoped(typeof(IQueueChannelProvider<>), typeof(QueueChannelProvider<>));

        // Register producer.
        services.AddScoped(typeof(IQueueProducer<>), typeof(QueueProducer<>));
    }

    public static void AddQueueMessageConsumer<TMessageConsumer, TQueueMessage>(this IServiceCollection services)
        where TMessageConsumer : IQueueConsumer<TQueueMessage> where TQueueMessage : class, IQueueMessage
    {
        // Register consumer.
        services.AddScoped(typeof(TMessageConsumer));
        services
            .AddScoped<IQueueConsumerHandler<TMessageConsumer, TQueueMessage>,
                QueueConsumerHandler<TMessageConsumer, TQueueMessage>>();

        // Register hosted service for queue listening.
        services.AddHostedService<QueueConsumerRegistrationService<TMessageConsumer, TQueueMessage>>();
    }
}