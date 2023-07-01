using Microsoft.Extensions.Options;
using ViteSharp.Models;

namespace ViteSharp;

public interface IViteAppManager
{
    ViteApp GetByAppName(string appName);
}

public class ViteAppManager : IViteAppManager
{ 
    private readonly Dictionary<string, ViteApp> _appDict = new();

    public ViteAppManager(IOptions<Dictionary<string, ViteAppConfig>> config, IViteManifestParser manifestParser)
    {
        foreach (var appConfig in config.Value)
        {
            _appDict?.Add(appConfig.Key, new ViteApp(appConfig.Value, manifestParser.GetEntryPoint(appConfig.Value.RootDirectory)));
        }
    }

    public ViteApp GetByAppName(string appName)
    {
        foreach (var key in _appDict.Keys)
        {
            if (appName == key) return _appDict[key];
        }

        throw new ArgumentNullException($"No app configuration was found with an app name of \"{appName}\". Please check your configuration.");
    }
    
}
