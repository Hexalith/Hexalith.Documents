// <copyright file="SumarizeDocument.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Commands.Documents;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents a command to generate a summary for a document.
/// </summary>
/// <param name="Id">The unique identifier of the document to summarize.</param>
[PolymorphicSerialization]
public partial record SummarizeDocument(string Id) : DocumentCommand(Id);