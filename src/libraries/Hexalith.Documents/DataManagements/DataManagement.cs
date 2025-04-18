// <copyright file="DataManagement.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.DataManagements;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

using Hexalith.Documents;
using Hexalith.Documents.Events.DataManagements;
using Hexalith.Domains;
using Hexalith.Domains.Results;

/// <summary>
/// Represents a data export management process that tracks the lifecycle and state of data exports.
/// Implements the domain aggregate pattern to maintain consistency and handle business rules.
/// </summary>
/// <param name="Id">The unique identifier for the data export management process.</param>
/// <param name="Size">The size of the exported data in bytes.</param>
/// <param name="Comments">Optional comments or notes about the data export process.</param>
/// <param name="StartedAt">The timestamp when the data export process was initiated.</param>
/// <param name="CompletedAt">The timestamp when the data export process was completed, null if not completed.</param>
[DataContract]
public record DataManagement(
    [property: DataMember(Order = 1)] string Id,
    [property: DataMember(Order = 2)] long Size,
    [property: DataMember(Order = 3)] string? Comments,
    [property: DataMember(Order = 4)] DateTimeOffset StartedAt,
    [property: DataMember(Order = 5)] DateTimeOffset? CompletedAt)
    : IDomainAggregate
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DataManagement"/> class.
    /// </summary>
    public DataManagement()
        : this(
              string.Empty,
              0L,
              null,
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
              null,
              started.DateTime,
              null)
    {
    }

    /// <inheritdoc/>
    /// <remarks>
    /// Returns the unique identifier of this data management process as the aggregate identifier.
    /// </remarks>
    public string AggregateId => Id;

    /// <inheritdoc/>
    /// <remarks>
    /// Returns the aggregate name for data management operations from the domain helper.
    /// </remarks>
    public string AggregateName => DocumentDomainHelper.DataManagementAggregateName;

    /// <inheritdoc/>
    /// <remarks>
    /// Applies domain events to update the state of the data management process.
    /// Supports events: DataExportStarted, DataExportCompleted, and DataManagementCommentsChanged.
    /// The data management must be initialized with DataExportStarted before other events can be applied.
    /// </remarks>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="domainEvent"/> is null.</exception>
    public ApplyResult Apply([NotNull] object domainEvent)
    {
        ArgumentNullException.ThrowIfNull(domainEvent);

        if (!(this as IDomainAggregate).IsInitialized() && domainEvent is not DataExportStarted)
        {
            return ApplyResult.Error(this, $"Operation not allowed: The data management operation with ID '{Id}' has not been initialized. Initialize the operation with a DataExportStarted event before applying other changes.");
        }

        return domainEvent switch
        {
            DataExportStarted e => ApplyEvent(e),
            DataExportCompleted e => ApplyEvent(e),
            DataManagementCommentsChanged e => ApplyEvent(e),
            DataManagementEvent => ApplyResult.NotImplemented(this),
            _ => ApplyResult.InvalidEvent(this, domainEvent),
        };
    }

    /// <inheritdoc/>
    /// <remarks>
    /// Checks if this data management process has been properly initialized.
    /// A data management is considered initialized when it has a non-empty identifier.
    /// </remarks>
    public bool IsInitialized() => !string.IsNullOrWhiteSpace(Id);

    private ApplyResult ApplyEvent(DataExportStarted e) => !IsInitialized()
        ? ApplyResult.Success(new DataManagement(e), [e])
        : ApplyResult.Error(this, $"Cannot start data export: A data export with ID '{Id}' already exists. Create a new data export with a unique identifier.");

    private ApplyResult ApplyEvent(DataExportCompleted e) => IsInitialized()
        ? ApplyResult.Success(this with { Size = e.Size, CompletedAt = e.DateTime }, [e])
        : ApplyResult.Error(this, $"Cannot complete data export: The data export with ID '{Id}' does not exist. Start the data export before marking it as complete.");

    private ApplyResult ApplyEvent(DataManagementCommentsChanged e) => e.Comments == Comments
        ? ApplyResult.Error(this, $"Cannot update comments: The new comments are identical to the existing comments for data export '{Id}'.")
        : ApplyResult.Success(this with { Comments = e.Comments }, [e]);
}