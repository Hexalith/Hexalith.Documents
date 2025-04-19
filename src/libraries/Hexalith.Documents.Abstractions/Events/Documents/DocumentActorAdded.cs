// <copyright file="DocumentActorAdded.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Events.Documents;

using Hexalith.Documents.ValueObjects;
using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents an event that occurs when an actor is added to a document.
/// </summary>
/// <param name="Id">The unique identifier of the document.</param>
/// <param name="Actor">The actor information being added to the document.</param>
[PolymorphicSerialization]
public partial record DocumentActorAdded(string Id, DocumentActor Actor)
    : DocumentEvent(Id)
{
}