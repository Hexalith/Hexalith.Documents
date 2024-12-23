﻿namespace Hexalith.Documents.Events.DocumentPartitions;

using System.Runtime.Serialization;

using Hexalith.Documents.Domain.ValueObjects;
using Hexalith.PolymorphicSerialization;

/// <summary>
/// Event that represents the addition of a document partition.
/// </summary>
/// <param name="Id">The identifier of the document partition.</param>
/// <param name="Name">The name of the document partition.</param>
/// <param name="Description">The description of the document partition.</param>
/// <param name="ConnectionString">The connection string name for the document partition.</param>
/// <param name="Targets">The targets associated with the document partition.</param>
[PolymorphicSerialization]
public partial record DocumentPartitionAdded(
    string Id,
    [property: DataMember(Order = 2)]
    string Name,
    [property: DataMember(Order = 3)]
    DocumentStorageType StorageType,
    [property: DataMember(Order = 4)]
    string? Description,
    [property: DataMember(Order = 5)]
    string ConnectionString)
    : DocumentPartitionEvent(Id)
{
}