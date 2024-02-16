using RabbitMQ.Client;

namespace Queuing.Interfaces;

internal interface IConnectionProvider
{
    IConnection GetConnection();
}