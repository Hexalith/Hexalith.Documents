// <copyright file="GetDocumentTypeDetails.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Requests.DocumentTypes;

using System.Runtime.Serialization;

using Hexalith.Application.Requests;
using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents a request to get the description of a document type by its ID.
/// </summary>
/// <param name="Id">The ID of the document type.</param>
/// <param name="Result">The result containing the ID and description.</param>
[PolymorphicSerialization]
public partial record GetDocumentTypeDetails(string Id, [property: DataMember(Order = 2)] DocumentTypeDetailsViewModel? Result = null)
    : DocumentTypeRequest(Id), IRequest
{
    /// <inheritdoc/>
    object? IRequest.Result => Result;
}