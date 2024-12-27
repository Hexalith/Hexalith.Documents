namespace Hexalith.Documents.Domain.DataManagements;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

using Hexalith.Documents.Domain;
using Hexalith.Documents.Events.DataManagements;
using Hexalith.Domain.Aggregates;

/// <summary>
/// Represents a data export process.
/// </summary>
[DataContract]
public record DataManagement(
    [property: DataMember(Order = 1)] string Id,
    [property: DataMember(Order = 2)] long Size,
    [property: DataMember(Order = 3)] DateTimeOffset StartedAt,
    [property: DataMember(Order = 4)] DateTimeOffset? CompletedAt)
    : IDomainAggregate
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DataManagement"/> class.
    /// </summary>
    public DataManagement()
        : this(
              string.Empty,
              0L,
              DateTimeOffset.Now,
              null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DataManagement"/> class.
    /// </summary>
    /// <param name="started">The event that started the data export.</param>
    public DataManagement(DataExportStarted started)
        : this(
              (started ?? throw new ArgumentNullException(nameof(started))).Id,
              0L,
              started.DateTime,
              null)
    {
    }

    /// <inheritdoc/>
    public string AggregateId => Id;

    /// <inheritdoc/>
    public string AggregateName => DocumentDomainHelper.DataManagementAggregateName;

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="domainEvent"/> is null.</exception>
    public ApplyResult Apply([NotNull] object domainEvent)
    {
        ArgumentNullException.ThrowIfNull(domainEvent);

        return domainEvent switch
        {
            DataExportStarted e => ApplyEvent(e),
            DataExportCompleted e => ApplyEvent(e),
            DataManagementEvent => ApplyResult.NotImplemented(this),
            _ => ApplyResult.InvalidEvent(this, domainEvent),
        };
    }

    /// <inheritdoc/>
    public bool IsInitialized() => !string.IsNullOrWhiteSpace(Id);

    private ApplyResult ApplyEvent(DataExportStarted e) => !IsInitialized()
        ? new ApplyResult(
            new DataManagement(e),
            [e],
            false)
        : new ApplyResult(this, [], true, "The data export document already exists.");

    private ApplyResult ApplyEvent(DataExportCompleted e)
    {
        if (IsInitialized())
        {
            return new ApplyResult(
                this with { Size = e.Size, CompletedAt = e.DateTime },
                [e],
                true);
        }

        return new ApplyResult(this, [], false, "The data export document does not exist.");
    }
}