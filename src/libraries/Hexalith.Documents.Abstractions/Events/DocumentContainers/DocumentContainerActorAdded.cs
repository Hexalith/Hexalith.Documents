﻿// <copyright file="DocumentContainerActorAdded.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Events.DocumentContainers;

using System.Runtime.Serialization;

using Hexalith.Documents.ValueObjects;
using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents an event that occurs when an actor is added to a document container.
/// </summary>
/// <param name="Id">The unique identifier of the document container.</param>
/// <param name="Actor">The actor being added to the document container.</param>
[PolymorphicSerialization]
public partial record DocumentContainerActorAdded(
    string Id,
    [property: DataMember(Order = 2)]
    DocumentActor Actor) : DocumentContainerEvent(Id);