namespace Hexalith.Documents.Commands.FileTypes;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerialization;

[PolymorphicSerialization]
public partial record FileTypeImport([property: DataMember(Order = 2)] string DocumentId) : FileTypeCommand(AggregateName)
{
}