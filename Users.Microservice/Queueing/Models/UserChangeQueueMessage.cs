﻿using Queuing.Interfaces;

namespace Users.Microservice.Queueing.Models;

public class UserChangeQueueMessage : IQueueMessage
{
    public Guid MessageId { get; set; }
    public TimeSpan TimeToLive { get; set; }
    
    public Guid UserGuid { get; set; }
    public string? Username { get; set; }
    public int ChangeType { get; set; }
    public string? ImageFullPath { get; set; }
}