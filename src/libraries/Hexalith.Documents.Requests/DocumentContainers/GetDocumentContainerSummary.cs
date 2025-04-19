// <copyright file="GetDocumentContainerSummary.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Requests.DocumentContainers;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents a request to get a document container summary.
/// </summary>
/// <param name="Id">The identifier of the document container.</param>
/// <param name="Result">The document container summary view model.</param>
[PolymorphicSerialization]
public partial record GetDocumentContainerSummary(
    string Id,
    [property: DataMember(Order = 3)] DocumentContainerSummaryViewModel? Result = null)
    : DocumentContainerRequest(Id);