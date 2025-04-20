// <copyright file="DataManagementSummaryViewModel.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Requests.DataManagements;

using System.Runtime.Serialization;

using Hexalith.Domains.ValueObjects;

/// <summary>
/// Represents a summary view model for data export.
/// </summary>
/// <param name="Id">The unique identifier of the data export.</param>
/// <param name="Size">The size of the data export.</param>
/// <param name="StartedAt">The date and time when the data export started.</param>
/// <param name="CompletedAt">The date and time when the data export completed.</param>
[DataContract]
public sealed record DataManagementSummaryViewModel(
    [property: DataMember(Order = 1)] string Id,
    [property: DataMember(Order = 2)] long Size,
    [property: DataMember(Order = 3)] DateTimeOffset StartedAt,
    [property: DataMember(Order = 3)] DateTimeOffset? CompletedAt) : IIdDescription
{
    /// <inheritdoc/>
    string IIdDescription.Description
    {
        get
        {
            if (CompletedAt is null)
            {
                return $"{Id} {StartedAt} ...";
            }
            return $"{Id} {StartedAt}/{CompletedAt} {Size / 1024m} Ko";
        }
    }

    /// <inheritdoc/>
    bool IIdDescription.Disabled => CompletedAt is null;
}