// <copyright file="DataManagementCommentsChangedOnDetailsProjectionHandler.cs" company="ITANEO">
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

public class DataManagementCommentsChangedOnDetailsProjectionHandler(IProjectionFactory<DataManagementDetailsViewModel> factory)
    : DataManagementDetailsProjectionHandler<DataManagementCommentsChanged>(factory)
{
    /// <inheritdoc/>
    protected override Task<DataManagementDetailsViewModel?> ApplyEventAsync(
        [NotNull] DataManagementCommentsChanged baseEvent,
        DataManagementDetailsViewModel? model,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        if (model == null)
        {
            return Task.FromResult<DataManagementDetailsViewModel?>(null);
        }

        return Task.FromResult<DataManagementDetailsViewModel?>(model with { Comments = baseEvent.Comments });
    }
}