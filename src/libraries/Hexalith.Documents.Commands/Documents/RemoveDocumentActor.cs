// <copyright file="RemoveDocumentActor.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Commands.Documents;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents a command to remove a document actor.
/// </summary>
/// <param name="Id">The unique identifier of the document.</param>
/// <param name="ContactId">The unique identifier of the contact associated with the document.</param>
[PolymorphicSerialization]
public partial record RemoveDocumentActor(string Id, string ContactId)
    : DocumentCommand(Id);