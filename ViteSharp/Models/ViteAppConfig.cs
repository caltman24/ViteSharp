namespace ViteSharp.Models;
public class ViteAppConfig
{
    public string Entrypoint { get; set; } = string.Empty;
    public string RootDirectory { get; set; } = "ViteApp";
    public int Port { get; set; } = 5173;
    public bool IsReact { get; set; }
}
