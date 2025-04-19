// <copyright file="GetFileTypeDetails.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Requests.FileTypes;

using System.Runtime.Serialization;

using Hexalith.Application.Requests;
using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents a request to get the details of a file type by its ID.
/// </summary>
/// <param name="Id">The unique identifier of the file type.</param>
/// <param name="Result">The result containing the file type details, if found.</param>
[PolymorphicSerialization]
public partial record GetFileTypeDetails(string Id, [property: DataMember(Order = 2)] FileTypeDetailsViewModel? Result = null)
    : FileTypeRequest(Id), IRequest
{
    /// <inheritdoc/>
    object? IRequest.Result => Result;
}