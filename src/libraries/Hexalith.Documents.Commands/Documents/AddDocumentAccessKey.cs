namespace Hexalith.Documents.Commands.Documents;

using System.Runtime.Serialization;

using Hexalith.Documents.Domain.ValueObjects;
using Hexalith.PolymorphicSerialization;

[PolymorphicSerialization]
public partial record AddDocumentAccessKey(
    string Id,
    [property: DataMember(Order = 2)] DocumentAccessKey AccessKey) : DocumentCommand(Id)
{
}