namespace Hexalith.Documents.Commands.FileTypes;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerializations;

[PolymorphicSerialization]
public partial record FileTypeExport([property: DataMember(Order = 2)] string UserId) : FileTypeCommand(AggregateName)
{
}