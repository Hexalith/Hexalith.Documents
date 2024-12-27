namespace Hexalith.Documents.Commands.FileTypes;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerialization;

[PolymorphicSerialization]
public partial record FileTypeExport([property: DataMember(Order = 2)] string UserId) : FileTypeCommand(AggregateName)
{
}