﻿namespace Hexalith.Documents.Commands;

using Hexalith.PolymorphicSerialization;

/// <summary>
/// Represents a document disabled event.
/// </summary>
[PolymorphicSerialization]
public partial record DisableDocument(string Id) : DocumentCommand(Id)
{
}