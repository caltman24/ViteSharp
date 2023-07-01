using Microsoft.AspNetCore.Hosting;
using Moq;

namespace ViteSharp.Tests;

public class ViteManifestParserTests
{
    private readonly IWebHostEnvironment _environment;
    private readonly IViteManifestParser _manifestParser;

    public ViteManifestParserTests()
    {
        _environment = Mock.Of<IWebHostEnvironment>();
        _manifestParser = new ViteManifestParser(_environment);
    }

    [Fact]
    public void GetEntryPoint_ShouldReturnManifest()
    {
        Mock.Get(_environment)
                .Setup(e => e.ContentRootPath)
                .Returns("");

        ViteManifest expected = new("assets/main.js",
                                    "src/main.tsx",
                                    isEntry: true,
                                    new[] { "assets/main.css", "assets/layout.css" },
                                    new[] { "assets/react.svg", "assets/vite.svg" });

        ViteManifest result = _manifestParser.GetEntryPoint("Client");

        Assert.Equivalent(expected, result, strict: true);
    }

    [Fact]
    public void GetEntryPoint_ThrowsFileNotFound()
    {
        Mock.Get(_environment)
                .Setup(e => e.ContentRootPath)
                .Returns("");

        //  {ContentRootPath}/wwwroot/Client/manifest.json
        ViteManifest action() => _manifestParser.GetEntryPoint("FileNotFound");

        var exception = Assert.Throws<FileNotFoundException>(action);
        Assert.StartsWith("No manifest file found in path", exception.Message);
    }

    [Fact]
    public void GetEntryPoint_ThrowsNoEntryPointFound()
    {
        Mock.Get(_environment)
                .Setup(e => e.ContentRootPath)
                .Returns("");

        //  {ContentRootPath}/wwwroot/Client/manifest.json
        ViteManifest action() => _manifestParser.GetEntryPoint("NoEntryClient");

        var exception = Assert.Throws<EntryPointNotFoundException>(action);
        Assert.StartsWith("No entrypoint found within the vite manifest", exception.Message);
    }
}
