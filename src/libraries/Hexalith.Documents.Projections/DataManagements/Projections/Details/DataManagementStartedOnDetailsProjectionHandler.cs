// <copyright file="DataManagementStartedOnDetailsProjectionHandler.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Projections.DataManagements.Projections.Details;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Projections;
using Hexalith.Documents.Events.DataManagements;
using Hexalith.Documents.Requests.DataManagements;

/// <summary>
/// Handles the projection update when a data export is added.
/// </summary>
/// <param name="factory">The factory.</param>
public class DataManagementStartedOnDetailsProjectionHandler(IProjectionFactory<DataManagementDetailsViewModel> factory)
    : DataManagementDetailsProjectionHandler<DataExportStarted>(factory)
{
    /// <inheritdoc/>
    protected override Task<DataManagementDetailsViewModel?> ApplyEventAsync([NotNull] DataExportStarted baseEvent, DataManagementDetailsViewModel? model, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        return Task.FromResult<DataManagementDetailsViewModel?>(new DataManagementDetailsViewModel(
            baseEvent.Id,
            0L,
            null,
            baseEvent.DateTime,
            null));
    }
}