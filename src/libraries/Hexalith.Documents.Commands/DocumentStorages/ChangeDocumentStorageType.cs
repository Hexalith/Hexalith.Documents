// <copyright file="ChangeDocumentStorageType.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Commands.DocumentStorages;

using System.Runtime.Serialization;

using Hexalith.Documents.ValueObjects;
using Hexalith.PolymorphicSerializations;

/// <summary>
/// Command to change the connection string name of a document storage.
/// </summary>
/// <param name="Id">The unique identifier of the document storage.</param>
/// <param name="StorageType">The type of the document storage.</param>
/// <param name="ConnectionString">The new connection string name.</param>
[PolymorphicSerialization]
public partial record ChangeDocumentStorageType(
    string Id,
    [property: DataMember(Order = 2)]
    DocumentStorageType StorageType,
    [property: DataMember(Order = 3)]
    string? ConnectionString)
    : DocumentStorageCommand(Id);