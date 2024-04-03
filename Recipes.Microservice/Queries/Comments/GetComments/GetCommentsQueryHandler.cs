using AutoMapper;
using MediatR;
using Recipes.Microservice.Common.Interfaces;
using Recipes.Microservice.Domain.Interfaces;

namespace Recipes.Microservice.Queries.Comments.GetComments;

public class GetCommentsQueryHandler : IRequestHandler<GetCommentsQuery, List<CommentDto>>
{
    #region Properties

    private readonly ICommentRepository _commentRepository;
    private readonly IMapper _mapper;
    private readonly ILoggerService _logger;

    #endregion

    #region Constructors

    public GetCommentsQueryHandler(ICommentRepository commentRepository,
        IMapper mapper, ILoggerService logger)
    {
        _commentRepository = commentRepository;
        _mapper = mapper;
        _logger = logger;
    }

    #endregion

    #region Methods

    public async Task<List<CommentDto>> Handle(GetCommentsQuery request, CancellationToken cancellationToken)
    {
        var result = new List<CommentDto>();

        try
        {
            var commentsInDb = await _commentRepository.GetByRecipeIdAsync(request.RecipeId, cancellationToken);

            result = _mapper.Map<List<CommentDto>>(commentsInDb);
        }
        catch (Exception ex)
        {
            _logger.Error(ex);
        }

        return result;
    }

    #endregion
}