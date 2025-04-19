// <copyright file="DocumentStorageConnectionStringChanged.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Events.DocumentStorages;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents an event that occurs when the connection string of a document storage is changed.
/// </summary>
/// <param name="Id">The unique identifier of the document storage.</param>
/// <param name="ConnectionString">The new connection string for the document storage.</param>
[PolymorphicSerialization]
public partial record DocumentStorageConnectionStringChanged(
    string Id,
    [property: DataMember(Order = 2)] string ConnectionString)
    : DocumentStorageEvent(Id)
{
}