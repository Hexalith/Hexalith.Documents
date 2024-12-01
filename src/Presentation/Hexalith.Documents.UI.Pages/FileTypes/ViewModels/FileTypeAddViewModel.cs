namespace Hexalith.Documents.UI.Pages.FileTypes.ViewModels;

using System.ComponentModel.DataAnnotations;

/// <summary>
/// ViewModel for adding a new document type.
/// </summary>
public class FileTypeAddViewModel
{
    /// <summary>
    /// Gets or sets the description of the document type.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the file to text converter for the document type.
    /// </summary>
    public string? FileToTextConverter { get; set; }

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

    /// <summary>
    /// Gets or sets the targets for the document type.
    /// </summary>
    public IEnumerable<string> Targets { get; set; } = [];
}