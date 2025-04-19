// <copyright file="FileTypeDetailsViewModel.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Requests.FileTypes;

using System.Collections.Generic;
using System.Runtime.Serialization;

using Hexalith.Domains.ValueObjects;

/// <summary>
/// Represents the details of a file type.
/// </summary>
/// <param name="Id">The unique identifier of the file type.</param>
/// <param name="Name">The name of the file type.</param>
/// <param name="ContentType">The primary MIME content type of the file.</param>
/// <param name="OtherContentTypes">Additional MIME content types that can be associated with this file type.</param>
/// <param name="FileExtension">The file extension associated with this file type.</param>
/// <param name="Comments">Additional comments or description of the file type.</param>
/// <param name="FileToTextConverter">The converter used to extract text from this file type.</param>
/// <param name="Disabled">Indicates whether the file type is disabled.</param>
[DataContract]
public sealed record FileTypeDetailsViewModel(
    [property: DataMember(Order = 1)] string Id,
    [property: DataMember(Order = 2)] string Name,
    [property: DataMember(Order = 3)] string ContentType,
    [property: DataMember(Order = 4)] IEnumerable<string> OtherContentTypes,
    [property: DataMember(Order = 5)] string FileExtension,
    [property: DataMember(Order = 6)] string? Comments,
    [property: DataMember(Order = 7)] string? FileToTextConverter,
    [property: DataMember(Order = 8)] bool Disabled) : IIdDescription
{
    /// <inheritdoc/>
    string IIdDescription.Description => Name;
}