using Microsoft.EntityFrameworkCore;
using Recipes.Microservice.Domain.Interfaces;
using Recipes.Microservice.Domain.Models;

namespace Recipes.Microservice.Infrastructure;

public class CommentRepository : Repository<Comment>, ICommentRepository
{
    public CommentRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
    {
    }

    public Task<List<Comment>> GetByRecipeIdAsync(long recipeId, CancellationToken cancellationToken)
    {
        return ApplicationDbContext.Comments.Where(c => c.RecipeId == recipeId)
            .Include(c => c.User)
            .ToListAsync(cancellationToken);
    }
    
    public Task<int> GetSumOfCommentRatingsByRecipeIdAsync(long recipeId, CancellationToken cancellationToken)
    {
        return ApplicationDbContext.Comments.Where(c => c.RecipeId == recipeId)
            .Select(c => c.Rating)
            .SumAsync(cancellationToken);
    }
    
    public Task<int> GetNumberOfCommentsByRecipeIdAsync(long recipeId, CancellationToken cancellationToken)
    {
        return ApplicationDbContext.Comments.Where(c => c.RecipeId == recipeId)
            .Select(c => c.Id)
            .CountAsync(cancellationToken);
    }
}