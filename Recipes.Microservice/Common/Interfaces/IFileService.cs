namespace Recipes.Microservice.Common.Interfaces;

public interface IFileService
{
    public Task<bool> SaveFileAsync(IFormFile file, string folder, string fileName);

    public string FormFileUrl(string path);

}