using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Recipes.Microservice.Common.Models;
using Recipes.Microservice.Infrastructure;

namespace Recipes.Microservice.Queries.Ingredient.GetIngredients;

public class GetIngredientsQueryHandler : IRequestHandler<GetIngredientsQuery, List<IngredientDto>>
{
    #region Properties

    private readonly Repository _repository;
    private readonly IMapper _mapper;

    #endregion

    #region Constructors

    public GetIngredientsQueryHandler(Repository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    #endregion

    #region Methods

    public async Task<List<IngredientDto>> Handle(GetIngredientsQuery request, CancellationToken cancellationToken)
    {
        var ingredients = await _repository.Ingredients.ToListAsync(cancellationToken);

        return _mapper.Map<List<Domains.Ingredient>, List<IngredientDto>>(ingredients);
    }

    #endregion
}