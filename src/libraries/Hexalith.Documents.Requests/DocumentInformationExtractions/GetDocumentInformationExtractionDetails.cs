// <copyright file="GetDocumentInformationExtractionDetails.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Requests.DocumentInformationExtractions;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents a request to get the description of a document information extraction by its ID.
/// </summary>
/// <param name="Id">The ID of the document information extraction.</param>
/// <param name="Result">The result containing the ID and description.</param>
[PolymorphicSerialization]
public partial record GetDocumentInformationExtractionDetails(string Id, [property: DataMember(Order = 2)] DocumentInformationExtractionDetailsViewModel? Result = null)
    : DocumentInformationExtractionRequest(Id);