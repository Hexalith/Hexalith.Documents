namespace Hexalith.Documents.Projections.DocumentContainers.Services;

using Hexalith.Documents.Commands.DocumentContainers;

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
    public static IEnumerable<CreateDocumentContainer> Data => [JohnDoe];

    /// <summary>
    /// Gets the details for the Excel document container.
    /// </summary>
    internal static CreateDocumentContainer JohnDoe => new(
        "john.doe@test.com",
        "Default",
        "John Doe Data",
        "john.doe@test.com",
        "John Doe default container for user files.",
        null);
}