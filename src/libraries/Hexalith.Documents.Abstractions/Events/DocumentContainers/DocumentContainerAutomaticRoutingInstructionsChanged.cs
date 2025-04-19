// <copyright file="DocumentContainerAutomaticRoutingInstructionsChanged.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Events.DocumentContainers;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents an event that occurs when the automatic routing instructions of a document container are changed.
/// </summary>
/// <param name="Id">The unique identifier of the document container.</param>
/// <param name="AutomaticRoutingInstructions">The new routing instructions, or null if instructions are being cleared.</param>
[PolymorphicSerialization]
public partial record DocumentContainerAutomaticRoutingInstructionsChanged(
    string Id,
    [property: DataMember(Order = 2)]
    string? AutomaticRoutingInstructions)
    : DocumentContainerEvent(Id);