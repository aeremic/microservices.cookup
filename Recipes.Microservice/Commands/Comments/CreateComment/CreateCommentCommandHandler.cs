using MediatR;
using Recipes.Microservice.Common.Interfaces;
using Recipes.Microservice.Domain.Interfaces;
using Recipes.Microservice.Domain.Models;

namespace Recipes.Microservice.Commands.Comments.CreateComment;

public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, bool>
{
    #region Properties

    private readonly ICommentRepository _commentRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILoggerService _logger;

    #endregion

    #region Constructors

    public CreateCommentCommandHandler(ICommentRepository commentRepository, IUserRepository userRepository,
        ILoggerService logger)
    {
        _commentRepository = commentRepository;
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