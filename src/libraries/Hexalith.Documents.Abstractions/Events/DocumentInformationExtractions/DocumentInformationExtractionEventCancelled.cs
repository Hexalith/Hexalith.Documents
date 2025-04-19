// <copyright file="DocumentInformationExtractionEventCancelled.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Events.DocumentInformationExtractions;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents an event that occurs when a document information extraction event is cancelled.
/// </summary>
/// <param name="Event">The original document information extraction event that was cancelled.</param>
/// <param name="Reason">The reason for cancelling the event.</param>
[PolymorphicSerialization]
public partial record DocumentInformationExtractionEventCancelled(
    [property: DataMember(Order = 2)]
    DocumentInformationExtractionEvent Event,
    [property: DataMember(Order = 3)]
    string Reason)
    : DocumentInformationExtractionEvent(Event.Id);