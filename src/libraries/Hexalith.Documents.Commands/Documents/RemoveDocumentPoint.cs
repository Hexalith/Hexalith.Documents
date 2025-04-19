// <copyright file="RemoveDocumentPoint.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Commands.Documents;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Command to remove a document point.
/// </summary>
/// <param name="Id">The unique identifier of the document point.</param>
/// <param name="Name">The name of the document point.</param>
[PolymorphicSerialization]
public partial record RemoveDocumentPoint(string Id, string Name)
    : DocumentCommand(Id);