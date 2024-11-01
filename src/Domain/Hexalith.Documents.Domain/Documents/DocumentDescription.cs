namespace Hexalith.Documents.Domain.Documents;

using System.Runtime.Serialization;

using Hexalith.Documents.Events;
using Hexalith.Domain.Aggregates;

/// <summary>
/// Represents the descriptive information of a document.
/// </summary>
[DataContract]
public record DocumentDescription(
    /// <summary>
    /// Gets the name of the document.
    /// </summary>
    /// <value>The document's display name.</value>
    [property: DataMember(Order = 1)] string Name,

    /// <summary>
    /// Gets the detailed description of the document.
    /// </summary>
    /// <value>The document's description text.</value>
    [property: DataMember(Order = 2)] string Description,

    /// <summary>
    /// Gets additional comments about the document.
    /// </summary>
    /// <value>The document's additional comments.</value>
    [property: DataMember(Order = 3)] string? Comments,

    /// <summary>
    /// Gets the type identifier of the document.
    /// </summary>
    /// <value>The document's type identifier string.</value>
    [property: DataMember(Order = 5)] string? DocumentTypeId,

    /// <summary>
    /// Gets a brief summary of the document's content.
    /// </summary>
    /// <value>The document's summary text.</value>
    [property: DataMember(Order = 6)] string? Summary)
{
    /// <summary>
    /// Applies a DocumentDescriptionChanged event to update the document's name and description.
    /// </summary>
    /// <param name="document">The document to update.</param>
    /// <param name="e">The DocumentDescriptionChanged event to apply.</param>
    /// <returns>The result of applying the event.</returns>
    internal static ApplyResult ApplyEvent(Document document, DocumentDescriptionChanged e)
        => (e.Name != document.Description.Name || e.Description != document.Description.Description)
            ? new ApplyResult(
                document with { Description = document.Description with { Name = e.Name, Description = e.Description } },
                [e],
                false)
            : new ApplyResult(document, [new DocumentEventCancelled(e, "The name and description are already set to the requested values.")], true);

    /// <summary>
    /// Applies a DocumentSummarized event to update the document's summary.
    /// </summary>
    /// <param name="document">The document to update.</param>
    /// <param name="e">The DocumentSummarized event to apply.</param>
    /// <returns>The result of applying the event.</returns>
    internal static ApplyResult ApplyEvent(Document document, DocumentSummarized e) => e.Summary != document.Description.Summary
        ? new ApplyResult(
            document with { Description = document.Description with { Summary = e.Summary } },
            [e],
            false)
        : new ApplyResult(document, [new DocumentEventCancelled(e, "The summary is already set to the requested value.")], true);
}