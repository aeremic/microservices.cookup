using NLog;
using Users.Microservice.Common.Interfaces;
using ILogger = NLog.ILogger;

namespace Users.Microservice.Common.Services;

public class LoggerService : ILoggerService
{
    private readonly ILogger _logger;

    public LoggerService()
    {
        _logger = LogManager.GetCurrentClassLogger();
    }

    public void Error(Exception ex)
    {
        _logger.Error(ex);
    }
}