namespace Recipes.Microservice.Common;

public interface IFileServiceHandler
{
    public bool HandleSaveFileAction(string folder, string fileName, string data);
}