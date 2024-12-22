namespace Hexalith.Documents.Projections.Documents.Services;

using Hexalith.Documents.Domain.Documents;
using Hexalith.Documents.Domain.ValueObjects;
using Hexalith.Documents.Requests.Documents;

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
    /// An enumerable collection of <see cref="DocumentDetailsViewModel"/> containing predefined documents.
    /// </value>
    public static IEnumerable<DocumentDetailsViewModel> Data => [Json];

    /// <summary>
    /// Gets the details for the JSON test document.
    /// </summary>
    internal static DocumentDetailsViewModel Json => new(
        "JsonTest1",
        new DocumentDescription("test1.json", "Test JSON document", null, "application/json", null),
        new DocumentRouting("john.doe@test.com", ["jane.doe@test.com"], ["bill.doe@test.com"]),
        null,
        new DocumentState(DateTimeOffset.UtcNow, "JsonTest1"),
        [new DocumentActor("john.doe@test.com", DocumentActorRole.Owner)],
        new FileDescription("test1", "test 1", "test1.json", 66587L, "application/json"),
        [new DocumentTag("Owner", "john.doe@test.com", true)],
        false);
}