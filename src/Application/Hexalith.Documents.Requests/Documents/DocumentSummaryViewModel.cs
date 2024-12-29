namespace Hexalith.Documents.Requests.Documents;

using System.Runtime.Serialization;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a summary view of a document type with essential information.
/// </summary>
/// <param name="Id">The unique identifier of the document type.</param>
/// <param name="Name">The name of the document type.</param>
/// <param name="Disabled">Indicates whether the document type is disabled.</param>
[DataContract]
[method: JsonConstructor]
public partial record DocumentSummaryViewModel(
    [property: DataMember(Order = 1)] string Id,
    [property: DataMember(Order = 2)] string Name,
    [property: DataMember(Order = 3)] long Size,
    [property: DataMember(Order = 4)] bool Disabled)
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DocumentSummaryViewModel"/> class from a <see cref="DocumentDetailsViewModel"/> object.
    /// </summary>
    /// <param name="details">The document type details to create the summary from.</param>
    /// <exception cref="ArgumentNullException">Thrown when details is null.</exception>
    public DocumentSummaryViewModel(DocumentDetailsViewModel details)
        : this(
              (details ?? throw new ArgumentNullException(nameof(details))).Id,
              details.Description.Name,
              details.File?.Size ?? 0L,
              details.Disabled)
    {
    }
}