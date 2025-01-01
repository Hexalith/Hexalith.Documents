namespace Hexalith.Documents.Requests.Documents;

using System.Collections.Generic;
using System.Runtime.Serialization;

using Hexalith.Application.Requests;
using Hexalith.Documents.Domain.Documents;
using Hexalith.Documents.Domain.ValueObjects;
using Hexalith.Domain.Aggregates;

[DataContract]
public partial record DocumentImportExportViewModel(
    [property: DataMember(Order = 1)] string Id,
    [property: DataMember(Order = 2)] DocumentDescription Description,
    [property: DataMember(Order = 3)] DocumentRouting? Routing,
    [property: DataMember(Order = 4)] string? ParentDocumentId,
    [property: DataMember(Order = 5)] DocumentState State,
    [property: DataMember(Order = 6)] IEnumerable<DocumentActor> Actors,
    [property: DataMember(Order = 7)] FileDescription? File,
    [property: DataMember(Order = 8)] IEnumerable<DocumentTag> Tags,
    [property: DataMember(Order = 9)] bool Disabled) : IExportModel
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
        if (aggregate is Document document)
        {
            return new DocumentImportExportViewModel(
                document.Id,
                document.Description,
                document.Routing,
                document.ParentDocumentId,
                document.State,
                document.Actors,
                document.File,
                document.Tags,
                document.Disabled);
        }

        throw new InvalidOperationException($"Invalid aggregate type: {aggregate.GetType().Name}. Expected: {nameof(Document)}.");
    }
}