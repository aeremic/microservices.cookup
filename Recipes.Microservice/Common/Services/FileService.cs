using Recipes.Microservice.Common.Interfaces;

namespace Recipes.Microservice.Common.Services;

public class FileService
{
    private IFileServiceHandler? _handler;

    public FileService()
    {
    }

    public FileService(IFileServiceHandler? handler)
    {
        _handler = handler;
    }

    public void SetFileServiceHandler(IFileServiceHandler? handler)
    {
        _handler = handler;
    }

    public async Task<bool> SaveFileAsync(IFormFile file, string folder, string fileName)
    {
        return await _handler?.HandleSaveFileActionAsync(file, folder, fileName)!;
    }

    public string GetFileUrl(string path)
    {
        return _handler?.HandleGetFileUrlActionAsync(path) ?? string.Empty;
    }
}