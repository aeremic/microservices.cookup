using MediatR;
using Recipes.Microservice.Common.Interfaces;
using Recipes.Microservice.Domain.Interfaces;
using Recipes.Microservice.Domain.Models;

namespace Recipes.Microservice.Commands.Comments.CreateComment;

public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, bool>
{
    #region Properties

    private readonly ICommentRepository _commentRepository;
    private readonly IRecipeRepository _recipeRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILoggerService _logger;

    #endregion

    #region Constructors

    public CreateCommentCommandHandler(ICommentRepository commentRepository, IRecipeRepository recipeRepository,
        IUserRepository userRepository,
        ILoggerService logger)
    {
        _commentRepository = commentRepository;
        _recipeRepository = recipeRepository;
        _userRepository = userRepository;
        _logger = logger;
    }

    #endregion

    #region Methods

    public async Task<bool> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        var result = false;

        try
        {
            var userInDb = await _userRepository.GetIdByGuidAsync(request.UserGuid, cancellationToken);
            if (userInDb != null)
            {
                await _commentRepository.AddAsync(new Comment
                {
                    UserId = userInDb.Id,
                    RecipeId = request.RecipeId,
                    Content = request.Content,
                    Rating = request.Rating,
                    CreatedOn = DateTime.UtcNow
                }, cancellationToken);

                var recipe = await _recipeRepository.GetAsync(request.RecipeId, cancellationToken);
                recipe.Rating = await CalculateRecipeRating(recipe.Id, cancellationToken);
                await _recipeRepository.UpdateAsync(recipe, cancellationToken);

                result = true;
            }
        }
        catch (Exception ex)
        {
            _logger.Error(ex);
        }

        return result;
    }

    private async Task<double?> CalculateRecipeRating(long recipeId, CancellationToken cancellationToken)
    {
        var sumOfComments =
            await _commentRepository.GetSumOfCommentRatingsByRecipeIdAsync(recipeId, cancellationToken);
        var numberOfComments =
            await _commentRepository.GetNumberOfCommentsByRecipeIdAsync(recipeId, cancellationToken);

        return sumOfComments / numberOfComments;
    }

    #endregion
}