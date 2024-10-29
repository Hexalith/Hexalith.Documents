namespace Hexalith.Documents.Shared.Documents.ViewModels;

/// <summary>
/// Represents the information needed to add a new document.
/// </summary>
public class DocumentAdd(string id, string name, string firstName, string lastName, string? phone, string? mobile, string? email, string? comments)
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DocumentAdd"/> class.
    /// </summary>
    public DocumentAdd() : this(string.Empty, string.Empty, string.Empty, string.Empty, null, null, null, null)
    {
    }

    /// <summary>
    /// Gets or sets the ID of the document.
    /// </summary>
    public string Id { get; set; } = id;

    /// <summary>
    /// Gets or sets the first name of the document.
    /// </summary>
    public string FirstName { get; set; } = firstName;

    /// <summary>
    /// Gets or sets the name of the document.
    /// </summary>
    public string Name { get; set; } = name;

    /// <summary>
    /// Gets or sets the last name of the document.
    /// </summary>
    public string LastName { get; set; } = lastName;

    /// <summary>
    /// Gets or sets the phone number of the document.
    /// </summary>
    public string? Phone { get; set; } = phone;

    /// <summary>
    /// Gets or sets the mobile number of the document.
    /// </summary>
    public string? Mobile { get; set; } = mobile;

    /// <summary>
    /// Gets or sets the email address of the document.
    /// </summary>
    public string? Email { get; set; } = email;

    /// <summary>
    /// Gets or sets comments of the document.
    /// </summary>
    public string? Comments { get; set; } = comments;
}
