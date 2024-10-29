namespace Hexalith.Documents.Shared.Documents.ViewModels;

/// <summary>
/// Represents the information needed to add a new document.
/// </summary>
public class DocumentAdd(string id, string name, string? description)
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DocumentAdd"/> class.
    /// </summary>
    public DocumentAdd()
        : this(string.Empty, string.Empty, null)
    {
    }

    /// <summary>
    /// Gets or sets the ID of the document.
    /// </summary>
    public string Id { get; set; } = id;

    /// <summary>
    /// Gets or sets the name of the document.
    /// </summary>
    public string Name { get; set; } = name;

    /// <summary>
    /// Gets or sets comments of the document.
    /// </summary>
    public string? Description { get; set; } = description;
}