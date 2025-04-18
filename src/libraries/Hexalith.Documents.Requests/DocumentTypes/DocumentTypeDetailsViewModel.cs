// <copyright file="DocumentTypeDetailsViewModel.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Requests.DocumentTypes;

using System.Collections.Generic;
using System.Runtime.Serialization;

using Hexalith.Documents.ValueObjects;
using Hexalith.Domains.ValueObjects;

/// <summary>
/// Represents the details of a document type.
/// </summary>
[DataContract]
public sealed record DocumentTypeDetailsViewModel(
    [property: DataMember(Order = 1)] string Id,
    [property: DataMember(Order = 2)] string Name,
    [property: DataMember(Order = 3)] string? Comments,
    [property: DataMember(Order = 4)] IEnumerable<string> DataExtractionIds,
    [property: DataMember(Order = 5)] IEnumerable<string> FileTypeIds,
    [property: DataMember(Order = 9)] IEnumerable<DocumentTag> Tags,
    [property: DataMember(Order = 6)] bool Disabled) : IIdDescription
{
    /// <inheritdoc/>
    string IIdDescription.Description => Name;
}