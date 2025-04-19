// <copyright file="DocumentTypeDescriptionChanged.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Events.DocumentTypes;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents an event that is raised when a document type's name and description are changed.
/// </summary>
/// <param name="Id">The unique identifier of the document type.</param>
/// <param name="Name">The new name of the document type.</param>
/// <param name="Description">The new description of the document type.</param>
[PolymorphicSerialization]
public partial record DocumentTypeDescriptionChanged(
    string Id,
    [property: DataMember(Order = 2)]
    string Name,
    [property: DataMember(Order = 3)]
    string? Description) : DocumentTypeEvent(Id);