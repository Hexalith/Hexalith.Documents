namespace Hexalith.Documents.WebServer.Infrastructure.Configurations;

using Hexalith.Application.Configurations;
using Hexalith.Documents.Domain;
using Hexalith.Extensions.Configuration;

/// <summary>
/// Class InvoiceSettings.
/// Implements the <see cref="ISettings" />.
/// </summary>
/// <seealso cref="ISettings" />
public class DocumentSettings : ISettings
{
    /// <summary>
    /// Gets or sets the command processor.
    /// </summary>
    /// <value>The command processor.</value>
    public CommandProcessorSettings? CommandProcessor { get; set; }

    /// <summary>
    /// The configuration section name of the settings.
    /// </summary>
    /// <returns>The name.</returns>
    public static string ConfigurationName() => DocumentDomainHelper.DocumentAggregateName;
}