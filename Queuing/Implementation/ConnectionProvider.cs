using Microsoft.Extensions.Logging;
using Queuing.Interfaces;
using RabbitMQ.Client;

namespace Queuing.Implementation;

internal sealed class ConnectionProvider : IDisposable, IConnectionProvider
{
    private IConnection? _connection;

    private readonly IConnectionFactory _connectionFactory;
    private readonly ILogger<ConnectionProvider> _logger;

    public ConnectionProvider(IConnectionFactory connectionFactory, ILogger<ConnectionProvider> logger)
    {
        _connectionFactory = connectionFactory;
        _logger = logger;
    }

    public void Dispose()
    {
        try
        {
            if (_connection is not { IsOpen: true })
            {
                return;
            }

            _connection.Close();
            _connection.Dispose();
            _logger.LogDebug("Closing connection.");
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Cannot dispose connection.");
        }
    }

    public IConnection GetConnection()
    {
        if (_connection is not { IsOpen: true })
        {
            _connection = _connectionFactory.CreateConnection();
            _logger.LogDebug("Opening connection.");
        }

        return _connection;
    }
}