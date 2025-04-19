// <copyright file="DocumentStorageDisabled.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Events.DocumentStorages;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents an event indicating that a document storage has been disabled.
/// </summary>
/// <param name="Id">The unique identifier of the document storage.</param>
[PolymorphicSerialization]
public partial record DocumentStorageDisabled(string Id) : DocumentStorageEvent(Id);