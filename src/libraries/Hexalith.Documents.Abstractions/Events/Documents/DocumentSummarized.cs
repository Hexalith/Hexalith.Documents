// <copyright file="DocumentSummarized.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Events.Documents;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents an event indicating that a document has been summarized.
/// </summary>
/// <param name="Id">The unique identifier of the document.</param>
/// <param name="Summary">The generated summary of the document content.</param>
[PolymorphicSerialization]
public partial record DocumentSummarized(string Id, string Summary) : DocumentEvent(Id);