// <copyright file="GetDocumentStorageDetails.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Requests.DocumentStorages;

using System.Runtime.Serialization;

using Hexalith.Application.Requests;
using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents a request to get the description of a document partition by its ID.
/// </summary>
/// <param name="Id">The ID of the document partition.</param>
/// <param name="Result">The result containing the ID and description.</param>
[PolymorphicSerialization]
public partial record GetDocumentStorageDetails(string Id, [property: DataMember(Order = 2)] DocumentStorageDetailsViewModel? Result = null)
    : DocumentStorageRequest(Id), IRequest
{
    /// <inheritdoc/>
    object? IRequest.Result => Result;
}