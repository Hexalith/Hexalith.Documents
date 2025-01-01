namespace Hexalith.Documents.Requests.DocumentInformationExtractions;

using System.Runtime.Serialization;

using Hexalith.Application.Requests;
using Hexalith.Documents.Domain.DocumentInformationExtractions;
using Hexalith.Domain.Aggregates;

/// <summary>
/// ViewModel for importing and exporting document information extraction.
/// </summary>
/// <param name="Id">The identifier.</param>
/// <param name="Name">The name.</param>
/// <param name="Model">The model.</param>
/// <param name="SystemMessage">The system message.</param>
/// <param name="OutputFormat">The output format.</param>
/// <param name="OutputSample">The output sample.</param>
/// <param name="Instructions">The instructions.</param>
/// <param name="ValidationModel">The validation model.</param>
/// <param name="ValidationInstructions">The validation instructions.</param>
/// <param name="Description">The description.</param>
/// <param name="Disabled">The disabled flag.</param>
[DataContract]
public partial record DocumentInformationExtractionImportExportViewModel(
    [property: DataMember(Order = 1)] string Id,
    [property: DataMember(Order = 2)] string Name,
    [property: DataMember(Order = 3)] string Model,
    [property: DataMember(Order = 3)] string SystemMessage,
    [property: DataMember(Order = 3)] string OutputFormat,
    [property: DataMember(Order = 3)] string OutputSample,
    [property: DataMember(Order = 3)] string Instructions,
    [property: DataMember(Order = 3)] string ValidationModel,
    [property: DataMember(Order = 3)] string ValidationInstructions,
    [property: DataMember(Order = 4)] string? Description,
    [property: DataMember(Order = 10)] bool Disabled) : IExportModel
{
    /// <summary>
    /// Creates the export model from the given aggregate.
    /// </summary>
    /// <param name="aggregate">The domain aggregate.</param>
    /// <returns>The export model.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the aggregate is null.</exception>
    /// <exception cref="ArgumentException">Thrown when the aggregate type is invalid.</exception>
    public static IExportModel CreateExportModel(IDomainAggregate aggregate)
    {
        ArgumentNullException.ThrowIfNull(aggregate, nameof(aggregate));
        if (aggregate is DocumentInformationExtraction extraction)
        {
            return new DocumentInformationExtractionImportExportViewModel(
                extraction.Id,
                extraction.Name,
                extraction.Model,
                extraction.SystemMessage,
                extraction.OutputFormat,
                extraction.OutputSample,
                extraction.Instructions,
                extraction.ValidationModel,
                extraction.ValidationInstructions,
                extraction.Description,
                extraction.Disabled);
        }

        throw new ArgumentException(
            $"Invalid aggregate type {aggregate.GetType().Name}. Expected {nameof(DocumentInformationExtraction)}.",
            nameof(aggregate));
    }
}