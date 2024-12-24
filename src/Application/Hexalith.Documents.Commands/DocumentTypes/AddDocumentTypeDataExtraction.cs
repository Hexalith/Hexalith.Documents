namespace Hexalith.Documents.Commands.DocumentTypes;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerialization;

[PolymorphicSerialization]
public partial record AddDocumentTypeDataExtraction(
    string Id,
    [property: DataMember(Order = 2)]
    string DataInformationExtractionId) : DocumentTypeCommand(Id)
{
}