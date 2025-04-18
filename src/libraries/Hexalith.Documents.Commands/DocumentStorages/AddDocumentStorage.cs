// <copyright file="AddDocumentStorage.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Commands.DocumentStorages;

using System.Runtime.Serialization;

using Hexalith.Documents.ValueObjects;
using Hexalith.PolymorphicSerializations;

[PolymorphicSerialization]
public partial record AddDocumentStorage(
    string Id,
    [property: DataMember(Order = 2)]
    string Name,
    [property: DataMember(Order = 3)]
    DocumentStorageType StorageType,
    [property: DataMember(Order = 4)]
    string? Comments,
    [property: DataMember(Order = 5)]
    string? ConnectionString)
    : DocumentStorageCommand(Id)
{
}