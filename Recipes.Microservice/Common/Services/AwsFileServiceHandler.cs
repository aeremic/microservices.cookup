using Recipes.Microservice.Common.Interfaces;

namespace Recipes.Microservice.Common.Services;

public class AwsFileServiceHandler : IFileServiceHandler
{
    public Task<bool> HandleSaveFileActionAsync(IFormFile file, string folder, string fileName)
    {
        throw new NotImplementedException();
    }

    public string HandleGetFileUrlActionAsync(string path)
    {
        throw new NotImplementedException();
    }
}