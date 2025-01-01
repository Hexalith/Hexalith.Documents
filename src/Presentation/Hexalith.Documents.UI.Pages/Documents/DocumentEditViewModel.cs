namespace Hexalith.Documents.UI.Pages.Documents;

using System.Collections.Generic;

using Hexalith.Application.Services;
using Hexalith.Documents.Domain.Documents;
using Hexalith.Documents.Domain.ValueObjects;
using Hexalith.Documents.Requests.Documents;
using Hexalith.Extensions.Helpers;

/// <summary>
/// ViewModel for editing document information extraction details.
/// </summary>
public sealed class DocumentEditViewModel : IIdDescription
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DocumentEditViewModel"/> class.
    /// </summary>
    /// <param name="details">The details of the document information extraction.</param>
    /// <exception cref="ArgumentNullException">Thrown when details is null.</exception>
    public DocumentEditViewModel(DocumentDetailsViewModel details)
    {
        ArgumentNullException.ThrowIfNull(details);
        Id = details.Id;
        Name = details.Description.Name;
        Comments = details.Description.Comments;
        DocumentTypeId = details.Description.DocumentTypeId;
        DocumentContainerId = details.Description.DocumentContainerId;
        Summary = details.Description.Summary;
        ParentDocumentId = details.ParentDocumentId;
        FromContactId = details.Routing?.FromContactId;
        ToContactIds = details.Routing?.ToContactIds;
        CopyToContactIds = details.Routing?.CopyToContactIds;
        Actors = details.Actors;
        Tags = details.Tags;
        Disabled = details.Disabled;
        SystemMessage = string.Empty;
        Original = details;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DocumentEditViewModel"/> class.
    /// </summary>
    public DocumentEditViewModel()
        : this(new DocumentDetailsViewModel(
            UniqueIdHelper.GenerateUniqueStringId(),
            new DocumentDescription(string.Empty, null, null, null, null),
            null,
            string.Empty,
            DocumentState.Create(DateTimeOffset.MinValue, string.Empty),
            [],
            null,
            [],
            false))
    {
    }

    /// <summary>
    /// Gets or sets the document actors.
    /// </summary>
    public IEnumerable<DocumentActor> Actors { get; set; }

    /// <summary>
    /// Gets or sets the comments.
    /// </summary>
    public string? Comments { get; set; }

    /// <summary>
    /// Gets or sets the copy to contact IDs.
    /// </summary>
    public IEnumerable<string>? CopyToContactIds { get; set; }

    /// <summary>
    /// Gets a value indicating whether the description has changed.
    /// </summary>
    public bool DescriptionChanged => Comments != Original.Description.Comments ||
        Name != Original.Description.Name ||
        DocumentTypeId != Original.Description.DocumentTypeId ||
        DocumentContainerId != Original.Description.DocumentContainerId ||
        Summary != Original.Description.Summary;

    /// <summary>
    /// Gets or sets a value indicating whether the item is disabled.
    /// </summary>
    public bool Disabled { get; set; }

    /// <summary>
    /// Gets the document container ID.
    /// </summary>
    public string DocumentContainerId { get; private set; }

    /// <summary>
    /// Gets or sets the document type ID.
    /// </summary>
    public string DocumentTypeId { get; set; }

    /// <summary>
    /// Gets the file description.
    /// </summary>
    public FileDescription? File => Original.File;

    /// <summary>
    /// Gets or sets the from contact ID.
    /// </summary>
    public string? FromContactId { get; set; }

    /// <summary>
    /// Gets a value indicating whether there are any changes.
    /// </summary>
    public bool HasChanges =>
        Id != Original.Id ||
        DescriptionChanged ||
        RoutingChanged ||
        ParentDocumentId != Original.ParentDocumentId ||
        Disabled != Original.Disabled;

    /// <summary>
    /// Gets or sets the ID.
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets the original details.
    /// </summary>
    public DocumentDetailsViewModel Original { get; }

    /// <summary>
    /// Gets or sets the parent document ID.
    /// </summary>
    public string? ParentDocumentId { get; set; }

    /// <summary>
    /// Gets a value indicating whether the routing has changed.
    /// </summary>
    public bool RoutingChanged => FromContactId != Original.Routing?.FromContactId ||
        ToContactIds != Original.Routing?.ToContactIds ||
        CopyToContactIds != Original.Routing?.CopyToContactIds;

    /// <summary>
    /// Gets the document state.
    /// </summary>
    public DocumentState State => Original.State;

    /// <summary>
    /// Gets or sets the summary.
    /// </summary>
    public string? Summary { get; set; }

    /// <summary>
    /// Gets or sets the system message.
    /// </summary>
    public string SystemMessage { get; set; }

    /// <summary>
    /// Gets or sets the document tags.
    /// </summary>
    public IEnumerable<DocumentTag> Tags { get; set; }

    /// <summary>
    /// Gets or sets the to contact IDs.
    /// </summary>
    public IEnumerable<string>? ToContactIds { get; set; }

    /// <inheritdoc/>
    string IIdDescription.Description => Name;
}