﻿using Users.Microservice.Domain.Models;

namespace Users.Microservice.Domain.Interfaces;

public interface IUserRepository
{
    Task<List<User>> GetAsync(CancellationToken cancellationToken);

    Task<User?> GetAsync(long id, CancellationToken cancellationToken);

    Task<User?> GetByGuidAsync(Guid guid, CancellationToken cancellationToken);
    
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);

    Task AddAsync(User user, CancellationToken cancellationToken);
    
    Task UpdateAsync(User user, CancellationToken cancellationToken);
}