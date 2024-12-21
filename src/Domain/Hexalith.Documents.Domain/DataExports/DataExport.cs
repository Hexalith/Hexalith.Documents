namespace Hexalith.Documents.Domain.DataExports;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

using Hexalith.Documents.Domain;
using Hexalith.Documents.Events.DataExports;
using Hexalith.Domain.Aggregates;

/// <summary>
/// Represents a data export process.
/// </summary>
[DataContract]
public record DataExport(
    [property: DataMember(Order = 1)] string Id,
    [property: DataMember(Order = 2)] DateTimeOffset StartedAt,
    [property: DataMember(Order = 3)] DateTimeOffset? CompletedAt)
    : IDomainAggregate
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DataExport"/> class.
    /// </summary>
    public DataExport()
        : this(
              string.Empty,
              DateTimeOffset.Now,
              null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DataExport"/> class.
    /// </summary>
    /// <param name="started">The event that started the data export.</param>
    public DataExport(DataExportStarted started)
        : this(
              (started ?? throw new ArgumentNullException(nameof(started))).Id,
              started.DateTime,
              null)
    {
    }

    /// <inheritdoc/>
    public string AggregateId => Id;

    /// <inheritdoc/>
    public string AggregateName => DocumentDomainHelper.DataExportAggregateName;

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="domainEvent"/> is null.</exception>
    public ApplyResult Apply([NotNull] object domainEvent)
    {
        ArgumentNullException.ThrowIfNull(domainEvent);

        return domainEvent switch
        {
            DataExportStarted e => ApplyEvent(e),
            DataExportCompleted e => ApplyEvent(e),
            DataExportEvent => ApplyResult.NotImplemented(this),
            _ => ApplyResult.InvalidEvent(this, domainEvent),
        };
    }

    /// <inheritdoc/>
    public bool IsInitialized() => !string.IsNullOrWhiteSpace(Id);

    private ApplyResult ApplyEvent(DataExportStarted e) => !IsInitialized()
        ? new ApplyResult(
            new DataExport(e),
            [e],
            false)
        : new ApplyResult(this, [], true, "The data export document already exists.");

    private ApplyResult ApplyEvent(DataExportCompleted e)
    {
        if (IsInitialized())
        {
            return new ApplyResult(
                this with { CompletedAt = e.DateTime },
                [e],
                true);
        }

        return new ApplyResult(this, [], false, "The data export document does not exist.");
    }
}