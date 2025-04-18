namespace Hexalith.Documents.Requests.DocumentInformationExtractions;

using System.Runtime.Serialization;

using Hexalith.Domain.ValueObjects;

/// <summary>
/// Represents the details of a document information extraction.
/// </summary>
[DataContract]
public sealed record DocumentInformationExtractionDetailsViewModel(
    [property: DataMember(Order = 1)] string Id,
    [property: DataMember(Order = 2)] string Name,
    [property: DataMember(Order = 3)] string Model,
    [property: DataMember(Order = 4)] string SystemMessage,
    [property: DataMember(Order = 5)] string OutputFormat,
    [property: DataMember(Order = 6)] string OutputSample,
    [property: DataMember(Order = 7)] string Instructions,
    [property: DataMember(Order = 8)] string ValidationModel,
    [property: DataMember(Order = 9)] string ValidationInstructions,
    [property: DataMember(Order = 10)] string? Comments,
    [property: DataMember(Order = 11)] bool Disabled) : IIdDescription
{
    /// <inheritdoc/>
    string IIdDescription.Description => Name;
}