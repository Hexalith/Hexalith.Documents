namespace Hexalith.Documents.Requests.DocumentContainers;

using System.Runtime.Serialization;

using Hexalith.Domain.ValueObjects;

[DataContract]
public sealed partial record DocumentContainerSummaryViewModel(
    [property: DataMember(Order = 1)] string Id,
    [property: DataMember(Order = 1)] string DocumentStorageId,
    [property: DataMember(Order = 2)] string Name,
    [property: DataMember(Order = 3)] bool Disabled) : IIdDescription
{
    /// <inheritdoc/>
    string IIdDescription.Description => Name;
}