using MediatR;
using Recipes.Microservice.Common.Interfaces;
using Recipes.Microservice.Domain.Interfaces;
using Recipes.Microservice.Domain.Models;

namespace Recipes.Microservice.Commands.Recipes.LikeRecipe;

public class LikeRecipeCommandHandler : IRequestHandler<LikeRecipeCommand, bool>
{
    #region Properties

    private readonly IUserRecipesRepository _userRecipesRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILoggerService _logger;

    #endregion

    #region Constructors

    public LikeRecipeCommandHandler(IUserRecipesRepository userRecipesRepository, IUserRepository userRepository, ILoggerService logger)
    {
        _userRecipesRepository = userRecipesRepository;
        _userRepository = userRepository;
        _logger = logger;
    }

    #endregion

    #region Methods

    public async Task<bool> Handle(LikeRecipeCommand request, CancellationToken cancellationToken)
    {
        var result = false;
        try
        {
            var userInDb = await _userRepository.GetByGuidAsync(request.UserGuid, cancellationToken);
            if (userInDb != null)
            {
                var userRecipeInDb = await _userRecipesRepository.GetAsync(userInDb.Id, request.RecipeId, cancellationToken);
                if (userRecipeInDb == null)
                {
                    await _userRecipesRepository.AddAsync(new UserRecipe
                    {
                        UserId = userInDb.Id,
                        RecipeId = request.RecipeId
                    }, cancellationToken);
                }
                else
                {
                    await _userRecipesRepository.RemoveAsync(userRecipeInDb, cancellationToken); 
                }

                result = true;
            }
        }
        catch (Exception ex)
        {
            _logger.Error(ex);
        }

        return result;
    }

    #endregion
}