// <copyright file="DocumentStatusManager.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Documents;

using System.Collections.Generic;

using Hexalith.Documents.ValueObjects;
using Hexalith.Domains;

/// <summary>
/// Manages the state transitions between different document statuses.
/// </summary>
/// <remarks>
/// This class implements a state machine that controls the valid transitions between document statuses.
/// The following transitions are allowed:
/// - Draft -> Final
/// - Final -> Draft, Validated
/// - Validated -> Draft, Final, Published
/// - Published -> Draft.
/// </remarks>
public record DocumentStatusManager : StateMachine<DocumentStatus>
{
    /// <summary>
    /// Gets the dictionary of valid state transitions between document statuses.
    /// </summary>
    /// <value>
    /// A dictionary where the key is the current document status and the value is a collection
    /// of valid target statuses that the document can transition to.
    /// </value>
    protected override IDictionary<DocumentStatus, IEnumerable<DocumentStatus>> ValidTransitions => new Dictionary<DocumentStatus, IEnumerable<DocumentStatus>>()
    {
        { DocumentStatus.Draft, [DocumentStatus.Final] },
        { DocumentStatus.Final, [DocumentStatus.Draft, DocumentStatus.Validated] },
        { DocumentStatus.Validated, [DocumentStatus.Draft, DocumentStatus.Final, DocumentStatus.Published] },
        { DocumentStatus.Published, [DocumentStatus.Draft, DocumentStatus.Final, DocumentStatus.Validated] },
    };
}