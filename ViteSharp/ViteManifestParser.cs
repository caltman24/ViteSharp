using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using ViteSharp.Models;

namespace ViteSharp;

public interface IViteManifestParser
{
    ViteManifest GetEntryPoint(string rootDirectory);
}

public class ViteManifestParser : IViteManifestParser
{
    private readonly IWebHostEnvironment _environment;

    public ViteManifestParser(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    public ViteManifest GetEntryPoint(string rootDirectory)
    {
        var fullPath = GetFullPath(rootDirectory);

        if (!File.Exists(fullPath))
        {
             throw new FileNotFoundException($"No manifest file found in path {fullPath}. Check the vite config file in the root directory of your vite app for manifest output on build. Must output to \"wwwroot/{{appRoot}}/manifest.json");
        }

        var jsonData = File.ReadAllText(fullPath);


        var manifest = JsonSerializer.Deserialize<Dictionary<string, ViteManifest>>(jsonData);

        var entrypoint = manifest?.SingleOrDefault(x => x.Value.isEntry.GetValueOrDefault()).Value
            ?? throw new EntryPointNotFoundException($"No entrypoint found within the vite manifest file at {fullPath}");

        return entrypoint;
    }

    private string GetFullPath(string appRoot)
    {
        var rootPath = _environment.ContentRootPath;
        return Path.Combine(rootPath, $"wwwroot/{appRoot}/manifest.json");
    }
}