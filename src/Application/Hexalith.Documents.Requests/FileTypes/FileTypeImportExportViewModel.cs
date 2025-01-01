namespace Hexalith.Documents.Requests.FileTypes;

using System.Collections.Generic;
using System.Runtime.Serialization;

using Hexalith.Application.Requests;
using Hexalith.Documents.Domain.FileTypes;
using Hexalith.Domain.Aggregates;

/// <summary>
/// Represents the details of a file type.
/// </summary>
/// <param name="Id">The unique identifier of the file type.</param>
/// <param name="Name">The name of the file type.</param>
/// <param name="Description">The description of the file type.</param>
/// <param name="FileToTextConverter">The file type file to text converter.</param>
/// <param name="Targets">The targets associated with the file type.</param>
/// <param name="Disabled">Indicates whether the file type is disabled.</param>
[DataContract]
public partial record FileTypeImportExportViewModel(
    [property: DataMember(Order = 1)] string Id,
    [property: DataMember(Order = 2)] string Name,
    [property: DataMember(Order = 3)] string? Description,
    [property: DataMember(Order = 4)] string? FileToTextConverter,
    [property: DataMember(Order = 5)] IEnumerable<string> Targets,
    [property: DataMember(Order = 6)] bool Disabled) : IExportModel
{
    /// <summary>
    /// Creates an export model from the specified domain aggregate.
    /// </summary>
    /// <param name="aggregate">The domain aggregate.</param>
    /// <returns>The export model.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the aggregate is null.</exception>
    /// <exception cref="ArgumentException">Thrown when the aggregate is not of type <see cref="FileType"/>.</exception>
    public static IExportModel CreateExportModel(IDomainAggregate aggregate)
    {
        ArgumentNullException.ThrowIfNull(aggregate);
        if (aggregate is FileType fileType)
        {
            return new FileTypeImportExportViewModel(
                fileType.Id,
                fileType.Name,
                fileType.Description,
                fileType.FileToTextConverter,
                fileType.Targets,
                fileType.Disabled);
        }

        throw new ArgumentException(
            $"Invalid aggregate type {aggregate.GetType().Name}. Expected {nameof(FileType)}.",
            nameof(aggregate));
    }
}