namespace Hexalith.Documents.Commands.DataManagements;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerialization;

[PolymorphicSerialization]
public partial record ChangeDataManagementComments(
    string Id,
    [property: DataMember(Order = 2)] string? Comments)
    : DataManagementCommand(Id);