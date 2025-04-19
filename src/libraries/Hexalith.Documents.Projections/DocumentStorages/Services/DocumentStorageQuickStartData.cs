// <copyright file="DocumentStorageQuickStartData.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Projections.DocumentStorages.Services;

using Hexalith.Documents.Commands.DocumentStorages;
using Hexalith.Documents.ValueObjects;

/// <summary>
/// Provides demo document storage data for testing and demonstration purposes.
/// </summary>
/// <remarks>
/// This static class contains predefined document storage configurations that can be used
/// during development, testing, and initial system setup. It includes various storage types
/// such as local file system and Azure cloud storage examples.
/// </remarks>
public static class DocumentStorageQuickStartData
{
    /// <summary>
    /// Gets a collection of sample document storage configurations.
    /// </summary>
    /// <value>
    /// An enumerable collection of <see cref="AddDocumentStorage"/> commands containing
    /// predefined document storage configurations for local and cloud storage.
    /// </value>
    public static IEnumerable<AddDocumentStorage> Data => [Documents, UserData];

    /// <summary>
    /// Gets a predefined document storage configuration for local file system storage.
    /// </summary>
    internal static AddDocumentStorage Documents => new(
            "Documents",
            "Storage for documents",
            DocumentStorageType.FileSystem,
            "The storage for documents using local storage",
            "/data/documents");

    /// <summary>
    /// Gets a predefined document storage configuration for user data storage.
    /// </summary>
    internal static AddDocumentStorage UserData => new(
            "UserData",
            "Storage for user's data",
            DocumentStorageType.FileSystem,
            "The storage for user's data using local storage",
            "/data/users");
}