namespace ViteSharp.Models;
public record ViteManifest(string file, string src, bool? isEntry, IEnumerable<string>? css, IEnumerable<string>? assets);