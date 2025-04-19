// <copyright file="DisableDocumentStorage.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Commands.DocumentStorages;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Command to disable a document storage.
/// </summary>
/// <param name="Id">The unique identifier of the document storage to disable.</param>
[PolymorphicSerialization]
public partial record DisableDocumentStorage(string Id)
    : DocumentStorageCommand(Id);