namespace Hexalith.Documents.Requests.DocumentInformationExtractions;

using System.Runtime.Serialization;

[DataContract]
public partial record DocumentInformationExtractionDetailsViewModel(
    [property: DataMember(Order = 1)] string Id,
    [property: DataMember(Order = 2)] string Name,
    [property: DataMember(Order = 3)] string Model,
    [property: DataMember(Order = 3)] string Instructions,
    [property: DataMember(Order = 4)] string? Description,
    [property: DataMember(Order = 5)] bool Disabled)
{
}