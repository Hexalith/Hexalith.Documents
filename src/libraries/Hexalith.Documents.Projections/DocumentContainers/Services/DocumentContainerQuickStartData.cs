// <copyright file="DocumentContainerQuickStartData.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Projections.DocumentContainers.Services;

using Hexalith.Documents.Commands.DocumentContainers;
using Hexalith.Documents.Projections.DocumentStorages.Services;

/// <summary>
/// Provides demo document container data for testing and demonstration purposes.
/// This static class contains sample document containers that can be used during development and testing.
/// </summary>
public static class DocumentContainerQuickStartData
{
    /// <summary>
    /// Gets a collection of sample document container details.
    /// </summary>
    /// <value>
    /// An enumerable collection of <see cref="CreateDocumentContainer"/> containing predefined document containers.
    /// </value>
    public static IEnumerable<CreateDocumentContainer> Data => [Triage, JohnDoe, JeromePiquot];

    /// <summary>
    /// Gets the details for the Jérôme Piquot document container.
    /// </summary>
    internal static CreateDocumentContainer JeromePiquot => new(
        "User-jpiquot@itaneo.com",
        DocumentStorageQuickStartData.UserData.Id,
        "Jérôme Piquot Data",
        "jpiquot@itaneo.com",
        "Jérôme Piquot default container for user files.",
        null);

    /// <summary>
    /// Gets the details for the Excel document container.
    /// </summary>
    internal static CreateDocumentContainer JohnDoe => new(
        "User-john.doe@test.com",
        DocumentStorageQuickStartData.UserData.Id,
        "John Doe Data",
        "john.doe@test.com",
        "John Doe default container for user files.",
        null);

    /// <summary>
    /// Gets the details for the Excel document container.
    /// </summary>
    internal static CreateDocumentContainer Triage => new(
        "Triage",
        DocumentStorageQuickStartData.Documents.Id,
        "Triage",
        "Triage",
        "Triage container for new documents.",
        null);
}