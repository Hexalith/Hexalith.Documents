// <copyright file="DocumentTypeQuickStartData.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Projections.DocumentTypes.Services;

using Hexalith.Documents.Commands.DocumentTypes;
using Hexalith.Documents.ValueObjects;

/// <summary>
/// Provides quick start data for document types.
/// </summary>
public static class DocumentTypeQuickStartData
{
    /// <summary>
    /// Gets the collection of predefined document types.
    /// </summary>
    /// <value>
    /// A collection of <see cref="AddDocumentType"/> commands to create predefined document types.
    /// </value>
    public static IEnumerable<AddDocumentType> Data => [Undefined, Export, Import, Triage, Quote];

    /// <summary>
    /// Gets the export document type.
    /// </summary>
    /// <value>
    /// An <see cref="AddDocumentType"/> command to create an export document type.
    /// </value>
    internal static AddDocumentType Export => new(
        "Export",
        "Export data file",
        "File containing exported data",
        [],
        [FileContentType.Json.Id]);

    /// <summary>
    /// Gets the import document type.
    /// </summary>
    /// <value>
    /// An <see cref="AddDocumentType"/> command to create an import document type.
    /// </value>
    internal static AddDocumentType Import => new(
        "Import",
        "Import data file",
        "File containing data to import",
        [],
        [FileContentType.Json.Id]);

    /// <summary>
    /// Gets the quote document type.
    /// </summary>
    /// <value>
    /// An <see cref="AddDocumentType"/> command to create a quote document type.
    /// </value>
    internal static AddDocumentType Quote => new(
        "Quote",
        "Quote",
        "Quote documents",
        [],
        ["Pdf"]);

    /// <summary>
    /// Gets the triage document type.
    /// </summary>
    /// <value>
    /// An <see cref="AddDocumentType"/> command to create a triage document type.
    /// </value>
    internal static AddDocumentType Triage => new(
        "Triage",
        "Triage",
        "Unprocessed triage documents",
        [],
        []);

    /// <summary>
    /// Gets the undefined document type.
    /// </summary>
    /// <value>
    /// An <see cref="AddDocumentType"/> command to create an undefined document type.
    /// </value>
    internal static AddDocumentType Undefined => new(
        "Undefined",
        "Undefined document",
        "Type for documents without any predefined type",
        [],
        []);
}