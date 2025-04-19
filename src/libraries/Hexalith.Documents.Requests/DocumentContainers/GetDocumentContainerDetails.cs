// <copyright file="GetDocumentContainerDetails.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Requests.DocumentContainers;

using System.Runtime.Serialization;

using Hexalith.Application.Requests;
using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents a request to get the description of a document container by its ID.
/// </summary>
/// <param name="Id">The ID of the document container.</param>
/// <param name="Result">The result containing the ID and description.</param>
[PolymorphicSerialization]
public partial record GetDocumentContainerDetails(string Id, [property: DataMember(Order = 2)] DocumentContainerDetailsViewModel? Result = null)
    : DocumentContainerRequest(Id), IRequest
{
    /// <inheritdoc/>
    object? IRequest.Result => Result;
}