using AutoMapper;
using Recipes.Microservice.Domain.Models;
using Recipes.Microservice.Queries.Comments.GetComments;

namespace Recipes.Microservice.Mappers;

public class CommentProfile : Profile
{
    public CommentProfile()
    {
        CreateMap<Comment, CommentDto>();
    }
}