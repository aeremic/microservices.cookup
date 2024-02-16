using Recipes.Microservice.Common.Interfaces;

namespace Recipes.Microservice.Common.Services.Storage;

public class FileService : IFileService
{
    private readonly IFileServiceHandler _handler;

    public FileService(IFileServiceHandler handler)
    {
        _handler = handler;
    }

    public async Task<bool> SaveFileAsync(IFormFile file, string folder, string fileName)
    {
        return await _handler.HandleSaveFileActionAsync(file, folder, fileName)!;
    }

    public string FormFileUrl(string path)
    {
        return _handler.HandleFormFileUrlActionAsync(path) ?? string.Empty;
    }
}