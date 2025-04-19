// <copyright file="DataManagementDetailsProjectionHandler{TDataManagementEvent}.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Projections.DataManagements.Projections.Details;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Metadatas;
using Hexalith.Application.Projections;
using Hexalith.Documents.Events.DataManagements;
using Hexalith.Documents.Requests.DataManagements;

/// <summary>
/// Abstract base class for handling updates to DataManagement projections based on events.
/// </summary>
/// <typeparam name="TDataManagementEvent">The type of the data export event.</typeparam>
/// <param name="factory">The actor projection factory.</param>
public abstract class DataManagementDetailsProjectionHandler<TDataManagementEvent>(IProjectionFactory<DataManagementDetailsViewModel> factory)
    : KeyValueProjectionUpdateEventHandlerBase<TDataManagementEvent, DataManagementDetailsViewModel>(factory)
    where TDataManagementEvent : DataManagementEvent
{
    /// <inheritdoc/>
    public override async Task ApplyAsync([NotNull] TDataManagementEvent baseEvent, Metadata metadata, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        ArgumentNullException.ThrowIfNull(metadata);

        DataManagementDetailsViewModel? currentValue = await GetProjectionAsync(metadata.AggregateGlobalId, cancellationToken)
            .ConfigureAwait(false);

        DataManagementDetailsViewModel? newValue = await ApplyEventAsync(
                baseEvent,
                currentValue,
                cancellationToken)
            .ConfigureAwait(false);
        if (newValue == null || newValue == currentValue)
        {
            return;
        }

        await SaveProjectionAsync(metadata.AggregateGlobalId, newValue, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Applies the event to the data export summary view model.
    /// </summary>
    /// <param name="baseEvent">The data export event.</param>
    /// <param name="model">The current data export detail view model.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated data export summary view model.</returns>
    protected abstract Task<DataManagementDetailsViewModel?> ApplyEventAsync(TDataManagementEvent baseEvent, DataManagementDetailsViewModel? model, CancellationToken cancellationToken);
}