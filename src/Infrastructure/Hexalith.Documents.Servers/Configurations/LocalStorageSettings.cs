namespace Hexalith.Documents.Servers.Configurations;

using Hexalith.Extensions.Configuration;

/// <summary>
/// Represents the settings for local storage.
/// </summary>
public class LocalStorageSettings : ISettings
{
    /// <summary>
    /// Gets or sets the path to the local storage.
    /// </summary>
    public string? Path { get; set; } = "/Storage";

    /// <summary>
    /// Gets the configuration name for local storage settings.
    /// </summary>
    /// <returns>The configuration name.</returns>
    public static string ConfigurationName() => $"{nameof(Hexalith)}:LocalStorage";
}