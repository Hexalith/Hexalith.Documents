// <copyright file="RemoveDocumentContainerActor.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Commands.DocumentContainers;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents an command that occurs when an actor is removed from a document container.
/// </summary>
/// <param name="Id">The unique identifier of the document container.</param>
/// <param name="ContactId">The identifier of the actor being removed.</param>
[PolymorphicSerialization]
public partial record RemoveDocumentContainerActor(
    string Id,
    [property: DataMember(Order = 2)]
    string ContactId) : DocumentContainerCommand(Id)
{
}