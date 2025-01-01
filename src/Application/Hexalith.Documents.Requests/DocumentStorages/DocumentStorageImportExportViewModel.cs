namespace Hexalith.Documents.Requests.DocumentStorages;

using System.Runtime.Serialization;

using Hexalith.Application.Requests;
using Hexalith.Documents.Domain.DocumentStorages;
using Hexalith.Documents.Domain.ValueObjects;
using Hexalith.Domain.Aggregates;

/// <summary>
/// ViewModel for importing and exporting document storage information.
/// </summary>
[DataContract]
public partial record DocumentStorageImportExportViewModel(
    [property: DataMember(Order = 1)] string Id,
    [property: DataMember(Order = 2)] string Name,
    [property: DataMember(Order = 3)] DocumentStorageType StorageType,
    [property: DataMember(Order = 4)] string? Description,
    [property: DataMember(Order = 5)] string ConnectionString,
    [property: DataMember(Order = 6)] bool Disabled) : IExportModel
{
    /// <summary>
    /// Creates an export model from the specified domain aggregate.
    /// </summary>
    /// <param name="aggregate">The domain aggregate.</param>
    /// <returns>An instance of <see cref="IExportModel"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the aggregate is null.</exception>
    /// <exception cref="ArgumentException">Thrown when the aggregate is not of type <see cref="DocumentStorage"/>.</exception>
    public static IExportModel CreateExportModel(IDomainAggregate aggregate)
    {
        ArgumentNullException.ThrowIfNull(aggregate);
        if (aggregate is DocumentStorage documentStorage)
        {
            return new DocumentStorageImportExportViewModel(
                documentStorage.Id,
                documentStorage.Name,
                documentStorage.StorageType,
                documentStorage.Description,
                documentStorage.ConnectionString,
                documentStorage.Disabled);
        }

        throw new ArgumentException(
            $"Invalid aggregate type {aggregate.GetType().Name}. Expected {nameof(DocumentStorage)}.",
            nameof(aggregate));
    }
}