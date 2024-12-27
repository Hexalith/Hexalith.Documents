namespace Hexalith.Documents.Requests.DocumentContainers;

using System.Runtime.Serialization;

[DataContract]
public partial record DocumentContainerSummaryViewModel(
    [property: DataMember(Order = 1)] string Id,
    [property: DataMember(Order = 1)] string StorageId,
    [property: DataMember(Order = 2)] string Name,
    [property: DataMember(Order = 3)] bool Disabled)
{
}