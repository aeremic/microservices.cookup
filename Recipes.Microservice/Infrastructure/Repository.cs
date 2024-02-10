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

    public void Add(TEntity entity)
    {
        ApplicationDbContext.Set<TEntity>().Add(entity);
    }

    public void Update(TEntity entity)
    {
        ApplicationDbContext.Set<TEntity>().Update(entity);
    }

    public void Remove(TEntity entity)
    {
        ApplicationDbContext.Set<TEntity>().Remove(entity);
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