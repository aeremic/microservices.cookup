using Recipes.Microservice.Domain.Models;

namespace Recipes.Microservice.Domain.Interfaces;

public interface IUserRecipesRepository
{
    Task AddAsync(UserRecipe userRecipe, CancellationToken cancellationToken);
}