﻿namespace Hexalith.Documents.Events.DocumentContainers;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerialization;

/// <summary>
/// Represents an event that occurs when a file type is added to a document container's supported file types.
/// </summary>
/// <param name="Id">The unique identifier of the document container.</param>
/// <param name="FileTypeId">The identifier of the file type being added.</param>
[PolymorphicSerialization]
public partial record DocumentContainerFileTypeAdded(
    string Id,
    [property: DataMember(Order = 2)]
    string FileTypeId)
    : DocumentContainerEvent(Id)
{
}