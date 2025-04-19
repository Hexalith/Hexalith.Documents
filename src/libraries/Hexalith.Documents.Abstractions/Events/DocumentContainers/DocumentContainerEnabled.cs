// <copyright file="DocumentContainerEnabled.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Events.DocumentContainers;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents an event that occurs when a document container is enabled.
/// </summary>
/// <param name="Id">The unique identifier of the document container.</param>
[PolymorphicSerialization]
public partial record DocumentContainerEnabled(string Id) : DocumentContainerEvent(Id)
{
}