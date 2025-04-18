namespace Hexalith.Documents.ValueObjects;

using System.Runtime.Serialization;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a file content type with an ID, MIME type, and file extension.
/// </summary>
[DataContract]
public sealed record FileContentType
(
    [property: DataMember(Order = 1)] string Id,
    [property: DataMember(Order = 2)] string Type,
    [property: DataMember(Order = 2)] string Extension)
{
    /// <summary>
    /// Gets the JSON file content type.
    /// </summary>
    [IgnoreDataMember]
    [JsonIgnore]
    public static FileContentType Json => new(nameof(Json), "application/json", "json");

    /// <summary>
    /// Gets the CSV file content type.
    /// </summary>
    [IgnoreDataMember]
    [JsonIgnore]
    public static FileContentType Csv => new(nameof(Csv), "text/csv", "csv");

    /// <summary>
    /// Gets the Excel file content type.
    /// </summary>
    [IgnoreDataMember]
    [JsonIgnore]
    public static FileContentType Excel => new(nameof(Excel), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "xlsx");

    /// <summary>
    /// Gets the HTML file content type.
    /// </summary>
    [IgnoreDataMember]
    [JsonIgnore]
    public static FileContentType Html => new(nameof(Html), "text/html", "html");

    /// <summary>
    /// Gets the Markdown file content type.
    /// </summary>
    [IgnoreDataMember]
    [JsonIgnore]
    public static FileContentType Markdown => new(nameof(Markdown), "text/markdown", "md");

    /// <summary>
    /// Gets the PDF file content type.
    /// </summary>
    [IgnoreDataMember]
    [JsonIgnore]
    public static FileContentType Pdf => new(nameof(Pdf), "application/pdf", "pdf");

    /// <summary>
    /// Gets the PowerPoint file content type.
    /// </summary>
    [IgnoreDataMember]
    [JsonIgnore]
    public static FileContentType PowerPoint => new(nameof(PowerPoint), "application/vnd.openxmlformats-officedocument.presentationml.presentation", "pptx");

    /// <summary>
    /// Gets the Text file content type.
    /// </summary>
    [IgnoreDataMember]
    [JsonIgnore]
    public static FileContentType Text => new(nameof(Text), "text/plain", "txt");

    /// <summary>
    /// Gets the Word file content type.
    /// </summary>
    [IgnoreDataMember]
    [JsonIgnore]
    public static FileContentType Word => new(nameof(Word), "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "docx");

    /// <summary>
    /// Gets the XML file content type.
    /// </summary>
    [IgnoreDataMember]
    [JsonIgnore]
    public static FileContentType Xml => new(nameof(Xml), "application/xml", "xml");
}