// <copyright file="AddDocumentStorage.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Commands.DocumentStorages;

using System.Runtime.Serialization;

using Hexalith.Documents.ValueObjects;

/// <summary>
/// Represents a command to add a document storage.
/// </summary>
/// <param name="Id">The unique identifier of the document storage.</param>
/// <param name="Name">The name of the document storage.</param>
/// <param name="StorageType">The type of the document storage.</param>
/// <param name="Comments">Optional comments about the document storage.</param>
/// <param name="ConnectionString">Optional connection string for the document storage.</param>
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
    : DocumentStorageCommand(Id);