namespace Hexalith.Documents.Events.DataManagements;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerialization;

[PolymorphicSerialization]
public partial record DataManagementCommentsChanged(
    string Id,
    [property: DataMember(Order = 2)] string? Comments)
    : DataManagementEvent(Id);