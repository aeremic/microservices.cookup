using Recipes.Microservice.Domain.Models;

namespace Recipes.Microservice.Domain.Interfaces;

public interface ICommentRepository
{
    Task AddAsync(Comment comment, CancellationToken cancellationToken);

    Task<List<Comment>> GetByRecipeIdAsync(long recipeId, CancellationToken cancellationToken);
}