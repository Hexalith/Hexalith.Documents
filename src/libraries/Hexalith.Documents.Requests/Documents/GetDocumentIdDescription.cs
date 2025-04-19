// <copyright file="GetDocumentIdDescription.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Requests.Documents;

using System.Runtime.Serialization;

using Hexalith.Domains.ValueObjects;
using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents a request to get the ID and description of a document.
/// </summary>
/// <param name="Id">The unique identifier of the document.</param>
/// <param name="Result">The result containing the document ID and description, if found.</param>
[PolymorphicSerialization]
public partial record GetDocumentIdDescription(string Id, [property: DataMember(Order = 2)] IdDescription? Result = null)
    : DocumentRequest(Id);