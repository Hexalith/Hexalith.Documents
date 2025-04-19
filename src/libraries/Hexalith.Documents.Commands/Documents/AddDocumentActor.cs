// <copyright file="AddDocumentActor.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Commands.Documents;

using Hexalith.Documents.ValueObjects;
using Hexalith.PolymorphicSerializations;

/// <summary>
/// Command to add a document actor to a document.
/// </summary>
/// <param name="Id">The unique identifier of the document.</param>
/// <param name="Actor">The actor to be added to the document.</param>
[PolymorphicSerialization]
public partial record AddDocumentActor(string Id, DocumentActor Actor)
    : DocumentCommand(Id);