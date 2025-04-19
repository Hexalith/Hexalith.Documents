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
/// <param name="Id">The unique identifier of the document type.</param>
/// <param name="Name">The display name of the document type.</param>
/// <param name="Comments">Additional comments or description of the document type.</param>
/// <param name="DataExtractionIds">The collection of data extraction identifiers associated with this document type.</param>
/// <param name="FileTypeIds">The collection of file type identifiers that can be used with this document type.</param>
/// <param name="Tags">The collection of tags associated with this document type.</param>
/// <param name="Disabled">Indicates whether the document type is disabled.</param>
[DataContract]
public sealed record DocumentTypeDetailsViewModel(
    [property: DataMember(Order = 1)] string Id,
    [property: DataMember(Order = 2)] string Name,
    [property: DataMember(Order = 3)] string? Comments,
    [property: DataMember(Order = 4)] IEnumerable<string> DataExtractionIds,
    [property: DataMember(Order = 5)] IEnumerable<string> FileTypeIds,
    [property: DataMember(Order = 6)] IEnumerable<DocumentTag> Tags,
    [property: DataMember(Order = 7)] bool Disabled) : IIdDescription
{
    /// <inheritdoc/>
    string IIdDescription.Description => Name;
}