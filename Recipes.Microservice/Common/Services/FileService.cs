using Recipes.Microservice.Common.Interfaces;

namespace Recipes.Microservice.Common.Services;

public class FileService
{
    private IFileServiceHandler? _handler;

    public IFileServiceHandler Handler
    {
        set => _handler = value;
    }

    public FileService()
    {
    }

    public async Task<bool> SaveFileAsync(IFormFile file, string folder, string fileName)
    {
        return await _handler?.HandleSaveFileActionAsync(file, folder, fileName)!;
    }

    public string FormFileUrl(string path)
    {
        return _handler?.HandleFormFileUrlActionAsync(path) ?? string.Empty;
    }
}