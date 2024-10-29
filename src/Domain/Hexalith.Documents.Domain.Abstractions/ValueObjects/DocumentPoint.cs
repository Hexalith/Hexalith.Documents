namespace Hexalith.Document.Domain.ValueObjects;

using System.Runtime.Serialization;

/// <summary>
/// Represents a document point for a person or entity.
/// This is a value object that encapsulates the details of a single point of document.
/// </summary>
/// <remarks>
/// A DocumentPoint can represent various types of document information such as phone numbers,
/// email addresses, or physical addresses. The specific type is determined by the PointType property.
/// </remarks>
[DataContract]
public record DocumentPoint(
    /// <summary>
    /// Gets or sets the name or identifier of the document point.
    /// </summary>
    /// <remarks>
    /// This could be used to distinguish between multiple document points of the same type,
    /// e.g., "Home Phone" vs "Work Phone".
    /// </remarks>
    [property: DataMember(Order = 1)]
    string Name,

    /// <summary>
    /// Gets or sets the type of the document point.
    /// </summary>
    /// <remarks>
    /// This property uses the DocumentPointType enum to specify the nature of the document information,
    /// such as Phone, Email, or PostalAddress.
    /// </remarks>
    [property: DataMember(Order = 2)]
    DocumentPointType PointType,

    /// <summary>
    /// Gets or sets the actual value of the document point.
    /// </summary>
    /// <remarks>
    /// This contains the specific document information, such as a phone number, email address, or physical address.
    /// The format of this value should correspond to the PointType.
    /// </remarks>
    [property: DataMember(Order = 3)]
    string Value)
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DocumentPoint"/> class.
    /// </summary>
    public DocumentPoint()
        : this(string.Empty, DocumentPointType.Phone, string.Empty)
    {
    }
}
