// <copyright file="DocumentDescription.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Documents;

using System.Runtime.Serialization;

using Hexalith.Documents.Events.Documents;
using Hexalith.Domains.Results;

/// <summary>
/// Represents the descriptive information of a document.
/// </summary>
/// <param name="Name">The name of the document.</param>
/// <param name="Comments">The comments associated with the document.</param>
/// <param name="DocumentContainerId">The unique identifier of the document container.</param>
/// <param name="DocumentTypeId">The unique identifier of the document type.</param>
/// <param name="Summary">The summary of the document.</param>
[DataContract]
public record DocumentDescription(
    [property: DataMember(Order = 1)] string Name,
    [property: DataMember(Order = 3)] string? Comments,
    [property: DataMember(Order = 4)] string? DocumentContainerId,
    [property: DataMember(Order = 5)] string? DocumentTypeId,
    [property: DataMember(Order = 6)] string? Summary)
{
    /// <summary>
    /// Gets an empty document description.
    /// </summary>
    public static DocumentDescription Empty => new(string.Empty, null, null, null, null);

    /// <summary>
    /// Applies a DocumentDescriptionChanged event to update the document's name and description.
    /// </summary>
    /// <param name="document">The document to update.</param>
    /// <param name="e">The DocumentDescriptionChanged event to apply.</param>
    /// <returns>The result of applying the event.</returns>
    internal static ApplyResult ApplyEvent(Document document, DocumentDescriptionChanged e)
        => e.Name != document.Description.Name || e.Comments != document.Description.Comments
            ? new ApplyResult(
                document with { Description = document.Description with { Name = e.Name, Comments = e.Comments } },
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