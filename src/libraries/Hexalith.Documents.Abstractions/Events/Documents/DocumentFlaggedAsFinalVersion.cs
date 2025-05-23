﻿// <copyright file="DocumentFlaggedAsFinalVersion.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Events.Documents;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents an event indicating that a document has been marked as the final version.
/// </summary>
/// <param name="Id">The unique identifier of the document.</param>
/// <param name="ByContactId">The identifier of the contact who flagged the document as final.</param>
/// <param name="Date">The date and time when the document was flagged as final.</param>
[PolymorphicSerialization]
public partial record DocumentFlaggedAsFinalVersion(string Id, string ByContactId, DateTimeOffset Date) : DocumentEvent(Id);