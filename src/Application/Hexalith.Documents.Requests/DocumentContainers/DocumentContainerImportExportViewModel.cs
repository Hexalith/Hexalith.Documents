namespace Hexalith.Documents.Requests.DocumentContainers;

using System.Collections.Generic;
using System.Runtime.Serialization;

using Hexalith.Application.Requests;
using Hexalith.Documents.Domain.DocumentContainers;
using Hexalith.Documents.Domain.ValueObjects;
using Hexalith.Domain.Aggregates;

/// <summary>
/// ViewModel for importing and exporting document containers.
/// </summary>
[DataContract]
public partial record DocumentContainerImportExportViewModel(
    [property: DataMember(Order = 1)] string Id,
    [property: DataMember(Order = 2)] string DocumentStorageId,
    [property: DataMember(Order = 3)] string Name,
    [property: DataMember(Order = 4)] string Path,
    [property: DataMember(Order = 5)] string? Comments,
    [property: DataMember(Order = 6)] string? AutomaticRoutingInstructions,
    [property: DataMember(Order = 7)] IEnumerable<DocumentActor> Actors,
    [property: DataMember(Order = 8)] IEnumerable<string> DocumentTypeIds,
    [property: DataMember(Order = 9)] IEnumerable<DocumentTag> Tags,
    [property: DataMember(Order = 10)] bool Disabled) : IExportModel
{
    /// <summary>
    /// Creates an export model from the specified domain aggregate.
    /// </summary>
    /// <param name="aggregate">The domain aggregate.</param>
    /// <returns>The export model.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the aggregate is null.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the aggregate is not of type DocumentContainer.</exception>
    public static IExportModel CreateExportModel(IDomainAggregate aggregate)
    {
        ArgumentNullException.ThrowIfNull(aggregate);
        if (aggregate is DocumentContainer documentContainer)
        {
            return new DocumentContainerImportExportViewModel(
                documentContainer.Id,
                documentContainer.DocumentStorageId,
                documentContainer.Name,
                documentContainer.Path,
                documentContainer.Comments,
                documentContainer.AutomaticRoutingInstructions,
                documentContainer.Actors,
                documentContainer.DocumentTypeIds,
                documentContainer.Tags,
                documentContainer.Disabled);
        }

        throw new InvalidOperationException($"Invalid aggregate type: {aggregate.GetType().Name}. Expected: {nameof(DocumentContainer)}.");
    }
}