﻿namespace Hexalith.Documents.Requests.DocumentContainers;

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
public partial record DocumentContainerSummaryViewModel(
    [property: DataMember(Order = 1)] string Id,
    [property: DataMember(Order = 2)] string Name,
    [property: DataMember(Order = 3)] bool Disabled)
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DocumentContainerSummaryViewModel"/> class from a <see cref="DocumentContainerDetailsViewModel"/> object.
    /// </summary>
    /// <param name="details">The document type details to create the summary from.</param>
    /// <exception cref="ArgumentNullException">Thrown when details is null.</exception>
    public DocumentContainerSummaryViewModel(DocumentContainerDetailsViewModel details)
        : this(
              (details ?? throw new ArgumentNullException(nameof(details))).Id,
              details.Name,
              details.Disabled)
    {
    }
}