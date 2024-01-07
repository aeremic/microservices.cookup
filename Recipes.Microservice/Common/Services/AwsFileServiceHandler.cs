using Recipes.Microservice.Common.Interfaces;

namespace Recipes.Microservice.Common.Services;

public class AwsFileServiceHandler : IFileServiceHandler
{
    #region Constructors

    public AwsFileServiceHandler()
    {
    }

    #endregion

    #region Methods

    public Task<bool> HandleSaveFileActionAsync(IFormFile file, string folder, string fileName)
    {
        throw new NotImplementedException();
    }

    public string HandleFormFileUrlActionAsync(string path)
    {
        throw new NotImplementedException();
    }

    #endregion
}