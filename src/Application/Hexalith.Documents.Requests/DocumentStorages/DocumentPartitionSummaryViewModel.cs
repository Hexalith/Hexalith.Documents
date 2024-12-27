namespace Hexalith.Documents.Requests.DocumentStorages;

using System.Runtime.Serialization;
using System.Text.Json.Serialization;

[DataContract]
[method: JsonConstructor]
public partial record DocumentStorageSummaryViewModel(
    [property: DataMember(Order = 1)] string Id,
    [property: DataMember(Order = 2)] string Name,
    [property: DataMember(Order = 3)] bool Disabled)
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DocumentStorageSummaryViewModel"/> class from a <see cref="DocumentStorageDetailsViewModel"/> object.
    /// </summary>
    /// <param name="details">The document type details to create the summary from.</param>
    /// <exception cref="ArgumentNullException">Thrown when details is null.</exception>
    public DocumentStorageSummaryViewModel(DocumentStorageDetailsViewModel details)
        : this(
              (details ?? throw new ArgumentNullException(nameof(details))).Id,
              details.Name,
              details.Disabled)
    {
    }
}