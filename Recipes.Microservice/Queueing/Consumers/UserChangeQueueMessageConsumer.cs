using Queuing.Interfaces;
using Recipes.Microservice.Queueing.Models;

namespace Recipes.Microservice.Queueing.Consumers;

public class UserChangeQueueMessageConsumer : IQueueConsumer<UserChangeQueueMessage>
{
    public UserChangeQueueMessageConsumer()
    {
    }

    public Task ConsumeAsync(UserChangeQueueMessage message)
    {
        return Task.CompletedTask;
    }
}