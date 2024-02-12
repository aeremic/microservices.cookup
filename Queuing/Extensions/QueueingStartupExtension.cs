using Microsoft.Extensions.DependencyInjection;
using Queuing.Implementation;
using Queuing.Interfaces;
using RabbitMQ.Client;

namespace Queuing.Extensions;

public static class QueueingStartupExtension
{
    public static void AddQueueing(this IServiceCollection services, QueueingConfigurationSettings settings)
    {
        services.AddSingleton(settings);

        services.AddSingleton<ConnectionFactory>(provider =>
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

        services.AddSingleton<IConnectionProvider, ConnectionProvider>();

        services.AddScoped<IChannelProvider, ChannelProvider>();
        services.AddScoped(typeof(IQueueChannelProvider<>), typeof(QueueChannelProvider<>));

        services.AddScoped(typeof(IQueueProducer<>), typeof(QueueProducer<>));
    }
}