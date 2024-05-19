using AutoMapper;
using MediatR;
using Recipes.Microservice.Common.Interfaces;
using Recipes.Microservice.Domain.Interfaces;

namespace Recipes.Microservice.Queries.Recipes.GetLikedRecipes;

public class GetLikedRecipesQueryHandler : IRequestHandler<GetLikedRecipesQuery, List<GetLikedRecipeDto>>
{
    #region Properties

    private readonly IUserRepository _userRepository;
    private readonly IRecipeRepository _recipeRepository;
    private readonly IMapper _mapper;
    private readonly IFileService _fileService;
    private readonly ILoggerService _logger;

    #endregion

    #region Constructors

    public GetLikedRecipesQueryHandler(IUserRepository userRepository, IRecipeRepository recipeRepository,
        IMapper mapper, IFileService fileService, ILoggerService logger)
    {
        _userRepository = userRepository;
        _recipeRepository = recipeRepository;
        _mapper = mapper;
        _fileService = fileService;

        _logger = logger;
    }

    #endregion

    #region Methods

    public async Task<List<GetLikedRecipeDto>> Handle(GetLikedRecipesQuery request, CancellationToken cancellationToken)
    {
        var result = new List<GetLikedRecipeDto>();
        try
        {
            var userInDb = await _userRepository.GetIdByGuidAsync(request.UserGuid, cancellationToken);
            if (userInDb != null)
            {
                var likedRecipes =
                    await _recipeRepository.GetRecipesWithUserRecipesByUserIdAsync(userInDb.Id, cancellationToken);

                result = _mapper.Map<List<GetLikedRecipeDto>>(likedRecipes);

                foreach (var item in result
                             .Where(item => !string.IsNullOrEmpty(item.ThumbnailPath)))
                {
                    item.ThumbnailPath = _fileService.FormFileUrl(item.ThumbnailPath ?? string.Empty);
                }
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