using RabbitMQ.Client;

namespace Queuing.Interfaces;

public interface IChannelProvider
{
    IModel GetChannel();
}