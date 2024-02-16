using Recipes.Microservice.Common.Interfaces;

namespace Recipes.Microservice.Common.Services.Storage;

public class LocalFileServiceHandler : IFileServiceHandler
{
    private readonly string? _hostingAddressValue;
    
    #region Constructors

    public LocalFileServiceHandler(IConfiguration configuration)
    {
        var hostingAddressSection = configuration.GetSection(
            Constants.HostingAddressConfigurationSectionKeys.HostingAddress);
        
        _hostingAddressValue = hostingAddressSection.GetSection(
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