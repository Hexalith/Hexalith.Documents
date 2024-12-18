﻿namespace Hexalith.Documents.Events.FileTypes;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerialization;

/// <summary>
/// Represents an event that sets the file-to-text converter for a file type.
/// </summary>
/// <param name="Id">The unique identifier of the file type.</param>
/// <param name="FileToTextConverter">The file-to-text converter to be set.</param>
[PolymorphicSerialization]
public partial record FileTypeFileToTextConverterChanged(
    string Id,
    [property: DataMember(Order = 2)] string? FileToTextConverter)
    : FileTypeEvent(Id)
{
}