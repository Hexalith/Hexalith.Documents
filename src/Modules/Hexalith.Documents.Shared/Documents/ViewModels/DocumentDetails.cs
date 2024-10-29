namespace Hexalith.Documents.Shared.Documents.ViewModels;
/// <summary>
/// Represents the details of a document in the system.
/// </summary>
/// <param name="Id">The unique identifier of the document.</param>
/// <param name="Name">The name of the document.</param>
/// <param name="Description">A description of the document.</param>
/// <param name="Person">The person associated with this document.</param>
/// <param name="DocumentPoints">A collection of document points for this document.</param>
/// <param name="Disabled">A flag indicating whether the document is disabled.</param>
public record DocumentDetails(
    string Id,
    string Name,
    string Description,
    bool Disabled);