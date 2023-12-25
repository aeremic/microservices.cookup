using Recipes.Microservice.Common.Interfaces;

namespace Recipes.Microservice.Common.Services;

public class AwsFileServiceHandler : IFileServiceHandler
{
    public bool HandleSaveFileAction(string folder, string fileName, string data)
    {
        throw new NotImplementedException();
    }
}