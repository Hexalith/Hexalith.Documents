namespace Hexalith.Documents.UI.Pages.DocumentTypes.ViewModels;

using System.ComponentModel.DataAnnotations;

/// <summary>
/// ViewModel for adding a new document type.
/// </summary>
public class DocumentTypeAddViewModel
{
    /// <summary>
    /// Gets or sets the description of the document type.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the list of file type identifiers.
    /// </summary>
    public IEnumerable<string> FileTypeIds { get; set; } = [];

    /// <summary>
    /// Gets or sets the unique identifier of the document type.
    /// </summary>
    [Required]
    public string? Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the document type.
    /// </summary>
    [Required]
    public string? Name { get; set; }
}