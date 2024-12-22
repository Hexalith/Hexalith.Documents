namespace Hexalith.Documents.Projections.DocumentTypes.Services;

using Hexalith.Documents.Commands.DocumentTypes;

/// <summary>
/// Provides demo document type data for testing and demonstration purposes.
/// This static class contains sample document types that can be used during development and testing.
/// </summary>
public static class DocumentTypeQuickStartData
{
    /// <summary>
    /// Gets a collection of sample document type details.
    /// </summary>
    /// <value>
    /// An enumerable collection of <see cref="AddDocumentType"/> containing predefined document types.
    /// </value>
    public static IEnumerable<AddDocumentType> Data => [Undefined];

    /// <summary>
    /// Gets the details for the Excel document type.
    /// </summary>
    internal static AddDocumentType Undefined => new(
        "Undefined",
        "Undefined document",
        "Type for documents without any predefined type",
        []);
}