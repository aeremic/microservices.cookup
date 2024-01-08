using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NLog;
using Users.Microservice.Common.Models;
using Users.Microservice.Common.Models.DTOs;
using Users.Microservice.Infrastructure;

namespace Users.Microservice.Queries.User.GetAllUsersData;

public class GetUsersDataQueryHandler : IRequestHandler<GetUsersDataQuery, List<UserDataDto>>
{
    #region Properties

    private readonly Repository _repository;
    private readonly IMapper _mapper;
    private readonly Logger _logger;

    #endregion

    #region Constructors

    public GetUsersDataQueryHandler(Repository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = LogManager.GetCurrentClassLogger();
    }

    #endregion

    #region Methods

    public async Task<List<UserDataDto>> Handle(GetUsersDataQuery request, CancellationToken cancellationToken)
    {
        var result = new List<UserDataDto>();
        try
        {
            var users = await _repository.Users.ToListAsync(cancellationToken);

            result = _mapper.Map<List<UserDataDto>>(users);
        }
        catch (Exception ex)
        {
            _logger.Error(ex);
        }

        return result;
    }

    #endregion
}