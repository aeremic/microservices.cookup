using Recipes.Microservice.Common.Interfaces;

namespace Recipes.Microservice.Common.Services;

public class LocalFileServiceHandler : IFileServiceHandler
{
    public async Task<bool> HandleSaveFileActionAsync(IFormFile file, string folder, string fileName)
    {
        var result = false;
        
        var path = Path.Combine(WebEnvironmentProvider.Host()?.WebRootPath ?? string.Empty, folder, fileName);
        
        await using var fileStream = new FileStream(path, FileMode.Create);
        await file.CopyToAsync(fileStream);
        result = true;
        
        return result;
    }

    public string HandleGetFileUrlActionAsync(string path)
    {
        return new Uri(new Uri("file://"), @path).ToString();
    }
}