namespace Hexalith.Documents.ApiServer.Controllers;

using Hexalith.Documents.Domain;
using Hexalith.Infrastructure.WebApis.Buses;

/// <summary>
/// Class CustomerEventsBusTopicAttribute. This class cannot be inherited.
/// Implements the <see cref="EventBusTopicAttribute" />.
/// </summary>
/// <seealso cref="EventBusTopicAttribute" />
[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
public sealed class FileTypeEventsBusTopicAttribute : EventBusTopicAttribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FileTypeEventsBusTopicAttribute"/> class.
    /// </summary>
    public FileTypeEventsBusTopicAttribute()
        : base(DocumentDomainHelper.FileTypeAggregateName)
    {
    }
}