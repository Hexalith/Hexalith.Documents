// <copyright file="GetDocumentTypeIdDescription.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Requests.DocumentTypes;

using System.Runtime.Serialization;

using Hexalith.Domains.ValueObjects;
using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents a request to get the ID and description of a document type.
/// </summary>
/// <param name="Id">The unique identifier of the document type.</param>
/// <param name="Result">The result containing the document type ID and description, if found.</param>
[PolymorphicSerialization]
public partial record GetDocumentTypeIdDescription(string Id, [property: DataMember(Order = 2)] IdDescription? Result = null)
    : DocumentTypeRequest(Id);