// <copyright file="DisableDocumentStorage.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Commands.DocumentStorages;

using Hexalith.PolymorphicSerializations;

[PolymorphicSerialization]
public partial record DisableDocumentStorage(string Id) : DocumentStorageCommand(Id)
{
}