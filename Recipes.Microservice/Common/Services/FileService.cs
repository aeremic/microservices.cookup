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

    public bool SaveFile(string folder, string fileName, string data)
    {
        return _handler?.HandleSaveFileAction(folder, fileName, data) ?? false;
    }
}