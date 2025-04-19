// <copyright file="DocumentTypeDisabled.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Events.DocumentTypes;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents an event that is raised when a document type is disabled in the system.
/// </summary>
/// <param name="Id">The unique identifier of the document type that was disabled.</param>
[PolymorphicSerialization]
public partial record DocumentTypeDisabled(string Id) : DocumentTypeEvent(Id)
{
}