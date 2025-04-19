// <copyright file="DocumentTypeEnabled.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Events.DocumentTypes;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents an event that is raised when a document type is enabled in the system.
/// </summary>
/// <param name="Id">The unique identifier of the document type that was enabled.</param>
[PolymorphicSerialization]
public partial record DocumentTypeEnabled(string Id) : DocumentTypeEvent(Id);