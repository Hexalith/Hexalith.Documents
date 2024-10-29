namespace Hexalith.Documents.Commands;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerialization;

[PolymorphicSerialization]
public partial record CreateDocument(
    string Id,
    [property: DataMember(Order = 2)]
    string Name,
    [property: DataMember(Order = 3)]
    string Description,
    [property: DataMember(Order = 4)]
    Uri LocationUrl,
    [property: DataMember(Order = 5)]
    string OwnerId,
    [property: DataMember(Order = 6)]
    DateTimeOffset CreatedOn,
    [property: DataMember(Order = 7)]
    string DocumentTypeId)
    : DocumentCommand(Id)
{
}