namespace Hexalith.Documents.Domain.DocumentPartitions;

using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

using Hexalith.Documents.Domain;
using Hexalith.Documents.Domain.ValueObjects;
using Hexalith.Documents.Events.DocumentPartitions;
using Hexalith.Domain.Aggregates;
using Hexalith.Domain.Events;

/// <summary>
/// Represents a partition of a document.
/// </summary>
/// <param name="Id">The identifier of the document partition.</param>
/// <param name="Name">The name of the document partition.</param>
/// <param name="StorageType">The storage type of the document partition.</param>
/// <param name="Description">The description of the document partition.</param>
/// <param name="ConnectionString">The name of the connection string.</param>
/// <param name="Tags">The tags associated with the document partition.</param>
/// <param name="Disabled">Indicates whether the document partition is disabled.</param>
[DataContract]
public record DocumentPartition(
    [property: DataMember(Order = 1)] string Id,
    [property: DataMember(Order = 2)] string Name,
    [property: DataMember(Order = 3)] DocumentStorageType StorageType,
    [property: DataMember(Order = 3)] string? Description,
    [property: DataMember(Order = 4)] string ConnectionString,
    [property: DataMember(Order = 5)] IEnumerable<DocumentTag> Tags,
    [property: DataMember(Order = 6)] bool Disabled)
    : IDomainAggregate
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DocumentPartition"/> class.
    /// </summary>
    public DocumentPartition()
        : this(
              string.Empty,
              string.Empty,
              DocumentStorageType.LocalFile,
              null,
              string.Empty,
              [],
              false)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DocumentPartition"/> class.
    /// </summary>
    /// <param name="added">The event that added the document partition.</param>
    public DocumentPartition(DocumentPartitionAdded added)
        : this(
              (added ?? throw new ArgumentNullException(nameof(added))).Id,
              added.Name,
              added.StorageType,
              added.Description,
              added.ConnectionString,
              [],
              false)
    {
    }

    /// <inheritdoc/>
    public string AggregateId => Id;

    /// <inheritdoc/>
    public string AggregateName => DocumentDomainHelper.DocumentPartitionAggregateName;

    /// <inheritdoc/>
    public ApplyResult Apply([NotNull] object domainEvent)
    {
        ArgumentNullException.ThrowIfNull(domainEvent);
        if (domainEvent is DocumentPartitionEvent ev && domainEvent is not DocumentPartitionEnabled && Disabled)
        {
            return new ApplyResult(
                this,
                [new DocumentPartitionEventCancelled(ev, $"Document container {Id}/{Name} is disabled.")],
                true);
        }

        return domainEvent switch
        {
            DocumentPartitionAdded e => ApplyEvent(e),
            DocumentPartitionConnectionStringNameChanged e => ApplyEvent(e),
            DocumentPartitionDescriptionChanged e => ApplyEvent(e),
            DocumentPartitionDisabled e => ApplyEvent(e),
            DocumentPartitionEnabled e => ApplyEvent(e),
            DocumentPartitionEvent e => new ApplyResult(
                this,
                [new DocumentPartitionEventCancelled(e, "Event not implemented")],
                true),
            _ => new ApplyResult(
                this,
                [
                    InvalidEventApplied.CreateNotSupportedAppliedEvent(
                    AggregateName,
                    AggregateId,
                    domainEvent),
                ],
                true),
        };
    }

    /// <inheritdoc/>
    public bool IsInitialized() => !string.IsNullOrWhiteSpace(Id);

    private ApplyResult ApplyEvent(DocumentPartitionAdded e) => !IsInitialized()
        ? new ApplyResult(
            new DocumentPartition(e),
            [e],
            false)
        : new ApplyResult(this, [new DocumentPartitionEventCancelled(e, $"The document container {Id}/{Name} already exists.")], true);

    private ApplyResult ApplyEvent(DocumentPartitionEnabled e) => Disabled
            ? new ApplyResult(
            this with { Disabled = false },
            [e],
            false)
            : new ApplyResult(this, [new DocumentPartitionEventCancelled(e, $"The document container {Id}/{Name} is already enabled.")], true);

    private ApplyResult ApplyEvent(DocumentPartitionDisabled e) => !Disabled
            ? new ApplyResult(
            this with { Disabled = true },
            [e],
            false)
            : new ApplyResult(this, [new DocumentPartitionEventCancelled(e, $"The document container {Id}/{Name} is already disabled.")], true);

    private ApplyResult ApplyEvent(DocumentPartitionDescriptionChanged e) => e.Name != Name || e.Description != Description
        ? new ApplyResult(
            this with { Name = e.Name, Description = e.Description },
            [e],
            false)
        : new ApplyResult(this, [], false);

    private ApplyResult ApplyEvent(DocumentPartitionConnectionStringNameChanged e)
    {
        if (ConnectionString == e.ConnectionStringName)
        {
            return new ApplyResult(this, [], false);
        }

        return new ApplyResult(
            this with { ConnectionString = e.ConnectionStringName },
            [e],
            false);
    }
}