// <copyright file="DocumentFlaggedAsDraft.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Events.Documents;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents an event that occurs when a document is flagged as draft.
/// </summary>
/// <param name="Id">The unique identifier of the document.</param>
/// <param name="ByContactId">The identifier of the contact who flagged the document as draft.</param>
/// <param name="Date">The date and time when the document was flagged as draft.</param>
[PolymorphicSerialization]
public partial record DocumentFlaggedAsDraft(string Id, string ByContactId, DateTimeOffset Date) : DocumentEvent(Id);