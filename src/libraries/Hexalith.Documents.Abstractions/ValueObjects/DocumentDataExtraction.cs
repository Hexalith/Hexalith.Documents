namespace Hexalith.Documents.ValueObjects;

using System.Runtime.Serialization;

/// <summary>
/// Represents the data extraction details of a document.
/// </summary>
/// <param name="Name">The name of the data extraction field.</param>
/// <param name="JsonFormat">The JSON format of the data extraction field.</param>
/// <param name="ExampleJson">The example JSON data extracted from the document.</param>
/// <param name="SystemMessage">The system message associated with the data extraction field.</param>
/// <param name="Prompt">The prompt message for the data extraction field.</param>
[DataContract]
public record class DocumentDataExtraction(
    [property: DataMember(Order = 1)]
    string Name,
    [property: DataMember(Order = 2)]
    string JsonFormat,
    [property: DataMember(Order = 3)]
    string ExampleJson,
    [property: DataMember(Order = 4)]
    string SystemMessage,
    [property: DataMember(Order = 5)]
    string Prompt)
{
}