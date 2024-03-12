using Microsoft.EntityFrameworkCore;
using Recipes.Microservice.Domain.Models;

namespace Recipes.Microservice.Infrastructure;

public abstract class Repository<TEntity> where TEntity : Entity
{
    protected readonly ApplicationDbContext ApplicationDbContext;

    protected Repository(ApplicationDbContext applicationDbContext)
    {
        ApplicationDbContext = applicationDbContext;
    }

    public Task AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        ApplicationDbContext.Set<TEntity>().Add(entity);
        
        return Task.FromResult(ApplicationDbContext.SaveChangesAsync(cancellationToken));
    }

    public Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        ApplicationDbContext.Set<TEntity>().Update(entity);
        
        return Task.FromResult(ApplicationDbContext.SaveChangesAsync(cancellationToken));
    }

    public Task RemoveAsync(TEntity entity, CancellationToken cancellationToken)
    {
        ApplicationDbContext.Set<TEntity>().Remove(entity);
        
        return Task.FromResult(ApplicationDbContext.SaveChangesAsync(cancellationToken));
    }

    public Task<TEntity?> GetAsync(long id, CancellationToken cancellationToken)
    {
        return ApplicationDbContext.Set<TEntity>().Where(e => e.Id == id).FirstOrDefaultAsync(cancellationToken);
    }

    public Task<List<TEntity>> GetAsync(CancellationToken cancellationToken)
    {
        return ApplicationDbContext.Set<TEntity>().ToListAsync(cancellationToken);
    }
}