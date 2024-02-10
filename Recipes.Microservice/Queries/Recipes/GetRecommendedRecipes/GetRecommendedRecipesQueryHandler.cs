﻿using AutoMapper;
using MediatR;
using NLog;
using Recipes.Microservice.Common;
using Recipes.Microservice.Common.Services;
using Recipes.Microservice.Domain.Interfaces;
using ILogger = NLog.ILogger;

namespace Recipes.Microservice.Queries.Recipes.GetRecommendedRecipes;

public class
    GetRecommendedRecipesQueryHandler : IRequestHandler<GetRecommendedRecipesQuery, List<GetRecommendedRecipeDto>>
{
    #region Properties

    private readonly IRecipeRepository _repository;
    private readonly IMapper _mapper;
    private readonly FileService _fileService;
    private readonly ILogger _logger;

    #endregion

    #region Constructors

    public GetRecommendedRecipesQueryHandler(IRecipeRepository repository, IMapper mapper, FileService fileService,
        IConfiguration configuration)
    {
        _repository = repository;
        _mapper = mapper;
        _fileService = fileService;

        _fileService.Handler =
            new LocalFileServiceHandler(
                configuration.GetSection(Constants.HostingAddressConfigurationSectionKeys.HostingAddress));
        _logger = LogManager.GetCurrentClassLogger();
    }

    #endregion

    #region Methods

    public async Task<List<GetRecommendedRecipeDto>> Handle(GetRecommendedRecipesQuery request,
        CancellationToken cancellationToken)
    {
        var result = new List<GetRecommendedRecipeDto>();
        try
        {
            if (request.PickedIngredients.Any())
            {
                var recipes =
                    await _repository.GetRecipesContainingIngredients(request.PickedIngredients, cancellationToken);

                result = _mapper.Map<List<GetRecommendedRecipeDto>>(recipes);

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