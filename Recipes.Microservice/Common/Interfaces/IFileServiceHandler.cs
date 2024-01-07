namespace Recipes.Microservice.Common.Interfaces;

public interface IFileServiceHandler
{
    Task<bool> HandleSaveFileActionAsync(IFormFile file, string folder, string fileName);
    public string HandleFormFileUrlActionAsync(string path);
}