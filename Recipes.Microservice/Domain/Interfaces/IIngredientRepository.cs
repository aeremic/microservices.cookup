using Recipes.Microservice.Domain.Models;

namespace Recipes.Microservice.Domain.Interfaces;

public interface IIngredientRepository
{
    Task<List<Ingredient>> GetAsync(CancellationToken cancellationToken);
}