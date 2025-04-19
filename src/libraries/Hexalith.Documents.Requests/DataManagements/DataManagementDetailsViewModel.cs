// <copyright file="DataManagementDetailsViewModel.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Requests.DataManagements;

using System.Runtime.Serialization;

using Hexalith.Domains.ValueObjects;

/// <summary>
/// Represents the details of a data export.
/// </summary>
/// <param name="Id">The unique identifier of the data export.</param>
/// <param name="Size">The size of the data export.</param>
/// <param name="Comments">Additional comments or description of the data export.</param>
/// <param name="StartedAt">The date and time when the data export started.</param>
/// <param name="CompletedAt">The date and time when the data export completed.</param>
[DataContract]
public sealed record DataManagementDetailsViewModel(
    [property: DataMember(Order = 1)] string Id,
    [property: DataMember(Order = 2)] long Size,
    [property: DataMember(Order = 3)] string? Comments,
    [property: DataMember(Order = 4)] DateTimeOffset StartedAt,
    [property: DataMember(Order = 5)] DateTimeOffset? CompletedAt) : IIdDescription
{
    /// <inheritdoc/>
    string IIdDescription.Description => Id;

    /// <inheritdoc/>
    bool IIdDescription.Disabled => CompletedAt is null;
}