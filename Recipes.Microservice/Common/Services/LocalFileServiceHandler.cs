﻿using Recipes.Microservice.Common.Interfaces;

namespace Recipes.Microservice.Common.Services;

public class LocalFileServiceHandler : IFileServiceHandler
{
    #region Constructors

    public LocalFileServiceHandler()
    {
    }

    #endregion

    #region Methods

    public async Task<bool> HandleSaveFileActionAsync(IFormFile file, string folder, string fileName)
    {
        var path = Path.Combine(WebEnvironmentProvider.Host()?.WebRootPath ?? string.Empty, folder, fileName);

        await using var fileStream = new FileStream(path, FileMode.Create);
        await file.CopyToAsync(fileStream);

        return true;
    }

    public string HandleFormFileUrlActionAsync(string path)
    {
        return new Uri(new Uri("file://"), @path).ToString();
    }

    #endregion
}