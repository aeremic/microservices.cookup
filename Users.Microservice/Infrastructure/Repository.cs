using Microsoft.EntityFrameworkCore;
using Users.Microservice.Domains.Models;

namespace Users.Microservice.Infrastructure;

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

    public Task Update(TEntity entity, CancellationToken cancellationToken)
    {
        ApplicationDbContext.Set<TEntity>().Update(entity);
        
        return Task.FromResult(ApplicationDbContext.SaveChangesAsync(cancellationToken));
    }

    public Task Remove(TEntity entity, CancellationToken cancellationToken)
    {
        ApplicationDbContext.Set<TEntity>().Remove(entity);
        
        return Task.FromResult(ApplicationDbContext.SaveChangesAsync(cancellationToken));
    }

    public Task<TEntity?> Get(long id, CancellationToken cancellationToken)
    {
        return ApplicationDbContext.Set<TEntity>().Where(e => e.Id == id).FirstOrDefaultAsync(cancellationToken);
    }

    public Task<List<TEntity>> Get(CancellationToken cancellationToken)
    {
        return ApplicationDbContext.Set<TEntity>().ToListAsync(cancellationToken);
    }
}