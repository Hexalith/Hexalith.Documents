// <copyright file="EnableDocument.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Commands.Documents;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents a document enabled event.
/// </summary>
/// <param name="Id">The unique identifier of the document to enable.</param>
[PolymorphicSerialization]
public partial record EnableDocument(string Id) : DocumentCommand(Id);