namespace Hexalith.Documents.Events.FileTypes;

using System.Runtime.Serialization;

using Hexalith.Application.States;
using Hexalith.Documents.Domain;
using Hexalith.PolymorphicSerialization;

/// <summary>
/// Represents an event that is raised when a file type event is cancelled.
/// </summary>
/// <param name="Event">The original file type event that was cancelled.</param>
/// <param name="Reason">The reason why the event was cancelled.</param>
[PolymorphicSerialization]
public partial record FileTypeEventCancelled(
    [property: DataMember(Order = 2)] MessageState Event,
    [property: DataMember(Order = 3)] string Reason)
{
    /// <summary>
    /// Gets the aggregate ID of the document command.
    /// </summary>
    public string AggregateId => Event.Metadata.Message.Aggregate.Id;

    /// <summary>
    /// Gets the aggregate name of the document command.
    /// </summary>
    public static string AggregateName => DocumentDomainHelper.FileTypeAggregateName;
}