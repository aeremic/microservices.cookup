using Microsoft.Extensions.Logging;
using Queuing.Interfaces;
using RabbitMQ.Client;

namespace Queuing.Implementation;

internal sealed class ChannelProvider : IDisposable, IChannelProvider
{
    private IChannel? _channel;

    private readonly IConnectionProvider _connectionProvider;
    private readonly ILogger<ChannelProvider> _logger;

    public ChannelProvider(IConnectionProvider connectionProvider, ILogger<ChannelProvider> logger)
    {
        _connectionProvider = connectionProvider;
        _logger = logger;
    }

    public void Dispose()
    {
        try
        {
            if (_channel is not { IsOpen: true })
            {
                return;
            }

            _channel.Close();
            _channel.Dispose();
            _logger.LogDebug($"Closing channel {_channel.ChannelNumber}.");
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Cannot dispose channel.");
        }
    }

    public IChannel GetChannel()
    {
        if (_channel is not { IsOpen: true })
        {
            _channel = _connectionProvider.GetConnection().CreateChannel();
            _logger.LogDebug("Opening connection.");
        }

        return _channel;
    }
}