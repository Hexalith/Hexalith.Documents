namespace Hexalith.Documents.Projections.DocumentContainers.Services;

using Hexalith.Documents.Domain.ValueObjects;
using Hexalith.Documents.Requests.DocumentContainers;

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
    /// An enumerable collection of <see cref="DocumentContainerDetailsViewModel"/> containing predefined document containers.
    /// </value>
    public static IEnumerable<DocumentContainerDetailsViewModel> Data => [JohnDoe];

    /// <summary>
    /// Gets the details for the Excel document container.
    /// </summary>
    internal static DocumentContainerDetailsViewModel JohnDoe => new(
        "john.doe@test.com",
        "John Doe Data",
        "John Doe default container for user files.",
        null,
        [new DocumentActor("john.doe@test.com", DocumentActorRole.Owner)],
        [],
        [new("Owner", "john.doe@test.com", true)], 
        false);
}