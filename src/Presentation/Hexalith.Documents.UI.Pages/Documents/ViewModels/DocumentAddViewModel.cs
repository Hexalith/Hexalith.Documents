namespace Hexalith.Documents.UI.Pages.Documents.ViewModels;

using Hexalith.Document.Domain.ValueObjects;
using Hexalith.Extensions.Helpers;

/// <summary>
/// ViewModel for adding a new document.
/// </summary>
public class DocumentAddViewModel
{
    /// <summary>
    /// Gets or sets the name of the container.
    /// </summary>
    public string? ContainerId { get; set; }

    /// <summary>
    /// Gets or sets the description of the document.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the document type.
    /// </summary>
    public string? DocumentTypeId { get; set; }

    /// <summary>
    /// Gets or sets the file description.
    /// </summary>
    public FileDescription? File { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the document.
    /// </summary>
    public string Id { get; set; } = UniqueIdHelper.GenerateUniqueStringId();

    /// <summary>
    /// Gets or sets the name of the document.
    /// </summary>
    public string? Name { get; set; }
}