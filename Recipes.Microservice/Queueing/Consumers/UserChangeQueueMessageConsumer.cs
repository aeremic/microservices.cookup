using NLog;
using Queuing.Interfaces;
using Recipes.Microservice.Common;
using Recipes.Microservice.Domain.Interfaces;
using Recipes.Microservice.Domain.Models;
using Recipes.Microservice.Queueing.Models;
using ILogger = NLog.ILogger;

namespace Recipes.Microservice.Queueing.Consumers;

public class UserChangeQueueMessageConsumer : IQueueConsumer<UserChangeQueueMessage>
{
    #region Properties

    private readonly IUserRepository _repository;
    private readonly ILogger _logger = LogManager.GetCurrentClassLogger(); // TODO: Change this to be injectable!

    #endregion

    #region Constructors

    public UserChangeQueueMessageConsumer(IUserRepository repository)
    {
        _repository = repository;
    }

    #endregion

    #region Methods

    public async Task ConsumeAsync(UserChangeQueueMessage message)
    {
        try
        {
            var user = new User
            {
                Guid = message.UserGuid,
                Username = message.Username,
                ImageFullPath = message.ImageFullPath
            };
            
            if (message.ChangeType == (int)Constants.UserChangeTypes.Added)
            {
                await _repository.AddAsync(user, default);
            }
        }
        catch (Exception ex)
        {
            _logger.Error(ex);
        }
    }

    #endregion
}