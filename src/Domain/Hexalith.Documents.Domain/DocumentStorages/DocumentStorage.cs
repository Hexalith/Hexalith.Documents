﻿namespace Hexalith.Documents.Domain.DocumentStorages;

using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

using Hexalith.Documents.Domain;
using Hexalith.Documents.Domain.ValueObjects;
using Hexalith.Documents.Events.DocumentStorages;
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
public record DocumentStorage(
    [property: DataMember(Order = 1)] string Id,
    [property: DataMember(Order = 2)] string Name,
    [property: DataMember(Order = 3)] DocumentStorageType StorageType,
    [property: DataMember(Order = 3)] string? Description,
    [property: DataMember(Order = 4)] string? ConnectionString,
    [property: DataMember(Order = 6)] bool Disabled)
    : IDomainAggregate
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DocumentStorage"/> class.
    /// </summary>
    public DocumentStorage()
        : this(
              string.Empty,
              string.Empty,
              DocumentStorageType.FileSystem,
              null,
              string.Empty,
              false)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DocumentStorage"/> class.
    /// </summary>
    /// <param name="added">The event that added the document partition.</param>
    public DocumentStorage(DocumentStorageAdded added)
        : this(
              (added ?? throw new ArgumentNullException(nameof(added))).Id,
              added.Name,
              added.StorageType,
              added.Description,
              added.ConnectionString,
              false)
    {
    }

    /// <inheritdoc/>
    public string AggregateId => Id;

    /// <inheritdoc/>
    public string AggregateName => DocumentDomainHelper.DocumentStorageAggregateName;

    /// <inheritdoc/>
    public ApplyResult Apply([NotNull] object domainEvent)
    {
        ArgumentNullException.ThrowIfNull(domainEvent);
        if (domainEvent is DocumentStorageEvent ev && domainEvent is not DocumentStorageEnabled && Disabled)
        {
            return new ApplyResult(
                this,
                [new DocumentStorageEventCancelled(ev, $"Document container {Id}/{Name} is disabled.")],
                true);
        }

        return domainEvent switch
        {
            DocumentStorageAdded e => ApplyEvent(e),
            DocumentStorageConnectionStringChanged e => ApplyEvent(e),
            DocumentStorageDescriptionChanged e => ApplyEvent(e),
            DocumentStorageDisabled e => ApplyEvent(e),
            DocumentStorageEnabled e => ApplyEvent(e),
            DocumentStorageEvent e => new ApplyResult(
                this,
                [new DocumentStorageEventCancelled(e, "Event not implemented")],
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

    private ApplyResult ApplyEvent(DocumentStorageAdded e) => !IsInitialized()
        ? new ApplyResult(
            new DocumentStorage(e),
            [e],
            false)
        : new ApplyResult(this, [new DocumentStorageEventCancelled(e, $"The document container {Id}/{Name} already exists.")], true);

    private ApplyResult ApplyEvent(DocumentStorageEnabled e) => Disabled
            ? new ApplyResult(
            this with { Disabled = false },
            [e],
            false)
            : new ApplyResult(this, [new DocumentStorageEventCancelled(e, $"The document container {Id}/{Name} is already enabled.")], true);

    private ApplyResult ApplyEvent(DocumentStorageDisabled e) => !Disabled
            ? new ApplyResult(
            this with { Disabled = true },
            [e],
            false)
            : new ApplyResult(this, [new DocumentStorageEventCancelled(e, $"The document container {Id}/{Name} is already disabled.")], true);

    private ApplyResult ApplyEvent(DocumentStorageDescriptionChanged e) => e.Name != Name || e.Comments != Description
        ? new ApplyResult(
            this with { Name = e.Name, Description = e.Comments },
            [e],
            false)
        : new ApplyResult(this, [], false);

    private ApplyResult ApplyEvent(DocumentStorageConnectionStringChanged e)
    {
        if (ConnectionString == e.ConnectionString)
        {
            return new ApplyResult(this, [], false);
        }

        return new ApplyResult(
            this with { ConnectionString = e.ConnectionString },
            [e],
            false);
    }
}