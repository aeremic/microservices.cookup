namespace Recipes.Microservice.Common.Interfaces;

public interface IFileServiceHandler
{
    public bool HandleSaveFileAction(string folder, string fileName, string data);
}