namespace Hexalith.Documents.Projections.Documents.Services;

using Hexalith.Documents.Commands.Documents;
using Hexalith.Documents.Domain.ValueObjects;

/// <summary>
/// Provides demo document data for testing and demonstration purposes.
/// This static class contains sample documents that can be used during development and testing.
/// </summary>
public static class DocumentQuickStartData
{
    /// <summary>
    /// Gets a collection of sample document details.
    /// </summary>
    /// <value>
    /// An enumerable collection of <see cref="AddDocument"/> containing predefined documents.
    /// </value>
    public static IEnumerable<AddDocument> Data => [Json];

    /// <summary>
    /// Gets the details for the JSON test document.
    /// </summary>
    internal static AddDocument Json => new(
        "JsonTest1",
        "test1.json",
        "Test JSON document",
        "/test",
        new FileDescription("test1", "test 1", "test1.json", 556849L, "application/json"),
        "john.doe@test.com",
        DateTimeOffset.UtcNow,
        "Unknown");
}