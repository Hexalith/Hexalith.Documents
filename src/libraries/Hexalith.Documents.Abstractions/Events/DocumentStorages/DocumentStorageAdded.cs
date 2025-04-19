// <copyright file="DocumentStorageAdded.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Events.DocumentStorages;

using System.Runtime.Serialization;

using Hexalith.Documents.ValueObjects;
using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents an event that occurs when a document storage is added.
/// </summary>
/// <param name="Id">The unique identifier of the document storage.</param>
/// <param name="Name">The name of the document storage.</param>
/// <param name="StorageType">The type of the document storage.</param>
/// <param name="Description">The description of the document storage.</param>
/// <param name="ConnectionString">The connection string for the document storage.</param>
[PolymorphicSerialization]
public partial record DocumentStorageAdded(
    string Id,
    [property: DataMember(Order = 2)] string Name,
    [property: DataMember(Order = 3)] DocumentStorageType StorageType,
    [property: DataMember(Order = 4)] string? Description,
    [property: DataMember(Order = 5)] string? ConnectionString) : DocumentStorageEvent(Id);