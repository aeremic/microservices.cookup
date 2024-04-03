using Recipes.Microservice.Domain.Interfaces;
using Recipes.Microservice.Domain.Models;

namespace Recipes.Microservice.Infrastructure;

public class IngredientRepository : Repository<Ingredient>, IIngredientRepository
{
    public IngredientRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
    {
    }
}