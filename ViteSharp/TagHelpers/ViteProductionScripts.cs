using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Hosting;

namespace ViteSharp.TagHelpers;

[HtmlTargetElement("vite-prod-scripts", TagStructure = TagStructure.WithoutEndTag)]
public class ViteProductionScripts : TagHelper
{
    private readonly IViteAppManager _appManager;
    private readonly IWebHostEnvironment _environment;

    [HtmlAttributeName("app-name")]
    public string? AppName { get; set; }

    public ViteProductionScripts(IViteAppManager appManager, IWebHostEnvironment environment)
    {
        _appManager = appManager;
        _environment = environment;
    }
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        if (_environment.IsProduction() is false) return;

        if (AppName is null)
        {
            throw new NullReferenceException(
                $"Attribute {nameof(AppName)} is missing from the vite development scripts tag helper"
            );
        }

        output.TagName = null;

        output.Content.SetHtmlContent(
           _appManager.GetByAppName(AppName).ProductionScripts
        ); ;
    }
}
