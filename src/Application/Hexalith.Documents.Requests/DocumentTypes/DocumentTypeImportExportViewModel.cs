namespace Hexalith.Documents.Requests.DocumentTypes;

using System.Collections.Generic;
using System.Runtime.Serialization;

using Hexalith.Application.Requests;
using Hexalith.Documents.Domain.DocumentTypes;
using Hexalith.Documents.Domain.ValueObjects;
using Hexalith.Domain.Aggregates;

/// <summary>
/// ViewModel for importing and exporting document types.
/// </summary>
[DataContract]
public partial record DocumentTypeImportExportViewModel(
    [property: DataMember(Order = 1)] string Id,
    [property: DataMember(Order = 2)] string Name,
    [property: DataMember(Order = 3)] string? Comments,
    [property: DataMember(Order = 7)] IEnumerable<string> DataExtractionIds,
    [property: DataMember(Order = 8)] IEnumerable<string> FileTypeIds,
    [property: DataMember(Order = 9)] IEnumerable<DocumentTag> Tags,
    [property: DataMember(Order = 10)] bool Disabled) : IExportModel
{
    /// <summary>
    /// Creates an export model from the specified aggregate.
    /// </summary>
    /// <param name="aggregate">The domain aggregate.</param>
    /// <returns>The export model.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the aggregate is null.</exception>
    public static IExportModel CreateExportModel(IDomainAggregate aggregate)
    {
        ArgumentNullException.ThrowIfNull(aggregate);
        if (aggregate is DocumentType documentType)
        {
            return new DocumentTypeImportExportViewModel(
                documentType.Id,
                documentType.Name,
                documentType.Comments,
                documentType.DataExtractionIds,
                documentType.FileTypeIds,
                documentType.Tags,
                documentType.Disabled);
        }

        throw new InvalidOperationException($"Invalid aggregate type: {aggregate.GetType().Name}. Expected: {nameof(DocumentType)}.");
    }
}