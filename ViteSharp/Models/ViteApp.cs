using System.Text;

namespace ViteSharp.Models;

public class ViteApp
{
    public string DevelopmentScripts { get; }
    public string ProductionScripts { get; }

    public ViteApp(ViteAppConfig viteConfig, ViteManifest manifest)
    {
        var baseUrl = $"http://localhost:{viteConfig.Port}";
        var viteDevClientScript = baseUrl + "/@vite/client";
        var viteDevEntrypoint = $"{baseUrl}/{viteConfig.Entrypoint}";
        var reactRefreshScript = $"import RefreshRuntime from '{baseUrl + "/@react-refresh"}';" +
                                "RefreshRuntime.injectIntoGlobalHook(window);" +
                                "window.$RefreshReg$ = () => {{}};" +
                                "window.$RefreshSig$ = () => (type) => type;" +
                                "window.__vite_plugin_react_preamble_installed__ = true;";

        var devScriptsBuilder = new StringBuilder();

        if (viteConfig.IsReact)
        {
            devScriptsBuilder.AppendFormat("<script type=\"module\">{0}</script>", reactRefreshScript);
        }

        devScriptsBuilder.AppendFormat("<script type=\"module\" src=\"{0}\"></script>", viteDevClientScript)
                         .AppendFormat("<script type=\"module\" src=\"{0}\"></script>", viteDevEntrypoint);

        var prodScriptsBuilder = new StringBuilder($"<script type=\"module\" src=\"{manifest.file}\"></script>");

        if (manifest!.css is not null)
        {
            foreach (var cssFile in manifest.css)
            {
                prodScriptsBuilder.AppendFormat("<link href=\"{0}\" rel=\"stylesheet\" />", cssFile);
            }
        }

        DevelopmentScripts = devScriptsBuilder.ToString();
        ProductionScripts = prodScriptsBuilder.ToString();
    }
}

