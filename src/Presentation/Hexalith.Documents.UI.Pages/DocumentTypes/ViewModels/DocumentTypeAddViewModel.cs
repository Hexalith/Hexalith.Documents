namespace Hexalith.Documents.UI.Pages.DocumentTypes.ViewModels;

using Hexalith.Extensions.Helpers;

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
    /// Gets or sets the unique identifier of the document type.
    /// </summary>
    public string Id { get; set; } = UniqueIdHelper.GenerateUniqueStringId();

    /// <summary>
    /// Gets or sets the name of the document type.
    /// </summary>
    public string? Name { get; set; }
}