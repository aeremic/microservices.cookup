using Recipes.Microservice.Common.Interfaces;

namespace Recipes.Microservice.Common.Services;

public class LocalFileServiceHandler : IFileServiceHandler
{
    private readonly string? _hostingAddressValue;
    
    #region Constructors

    public LocalFileServiceHandler()
    {
    }
    
    public LocalFileServiceHandler(IConfiguration configurationSection)
    {
        _hostingAddressValue = configurationSection.GetSection(
            Constants.HostingAddressConfigurationSectionKeys.Value).Value ?? string.Empty;
    }

    #endregion

    #region Methods

    public async Task<bool> HandleSaveFileActionAsync(IFormFile file, string folderPath, string fileName)
    {
        var path = Path.Combine(_hostingAddressValue ?? string.Empty, folderPath, fileName);

        await using var fileStream = new FileStream(path, FileMode.Create);
        await file.CopyToAsync(fileStream);

        return true;
    }

    public string HandleFormFileUrlActionAsync(string filePath)
    {
        return Path.Combine(_hostingAddressValue ?? string.Empty, filePath);
    }

    #endregion
}