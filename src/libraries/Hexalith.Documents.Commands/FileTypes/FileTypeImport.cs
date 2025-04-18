namespace Hexalith.Documents.Commands.FileTypes;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerializations;

[PolymorphicSerialization]
public partial record FileTypeImport([property: DataMember(Order = 2)] string DocumentId) : FileTypeCommand(AggregateName)
{
}