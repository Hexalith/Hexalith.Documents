namespace Hexalith.Documents.Projections.FileTypes.Services;

using Hexalith.Documents.Commands.FileTypes;
using Hexalith.Documents.Domain.ValueObjects;

/// <summary>
/// Provides demo file type data for testing and demonstration purposes.
/// This static class contains sample file types that can be used during development and testing.
/// </summary>
public static class FileTypeQuickStartData
{
    /// <summary>
    /// Gets a collection of sample file type details.
    /// </summary>
    /// <value>
    /// An enumerable collection of <see cref="AddFileType"/> containing predefined file types.
    /// </value>
    public static IEnumerable<AddFileType> Data => [Excel, Html, Markdown, Pdf, PowerPoint, Text, Word, Json, Xml, Csv];

    /// <summary>
    /// Gets the details for the CSV file type.
    /// </summary>
    internal static AddFileType Csv => new(
        FileContentType.Csv.Id,
        "CSV file",
        FileContentType.Csv.Type,
        [],
        FileContentType.Csv.Extension,
        null,
        null);

    /// <summary>
    /// Gets the details for the Excel file type.
    /// </summary>
    internal static AddFileType Excel => new(
        FileContentType.Excel.Id,
        "Microsoft Excel",
        FileContentType.Excel.Type,
        [],
        FileContentType.Excel.Extension,
        "Type for Microsoft Excel files",
        null);

    /// <summary>
    /// Gets the details for the HTML file type.
    /// </summary>
    internal static AddFileType Html => new(
        FileContentType.Html.Id,
        "HTML file",
        FileContentType.Html.Type,
        [],
        FileContentType.Html.Extension,
        "Type for HTML files",
        null);

    /// <summary>
    /// Gets the details for the Json file type.
    /// </summary>
    internal static AddFileType Json => new(
        FileContentType.Json.Id,
        "Json file",
        FileContentType.Json.Type,
        [],
        FileContentType.Json.Extension,
        null,
        null);

    /// <summary>
    /// Gets the details for the Markdown file type.
    /// </summary>
    internal static AddFileType Markdown => new(
        FileContentType.Markdown.Id,
        "Markdown file",
        FileContentType.Markdown.Type,
        [],
        FileContentType.Markdown.Extension,
        "Type for Markdown files",
        null);

    /// <summary>
    /// Gets the details for the PDF file type.
    /// </summary>
    internal static AddFileType Pdf => new(
        FileContentType.Pdf.Id,
        "Adobe PDF",
        FileContentType.Pdf.Type,
        [],
        FileContentType.Pdf.Extension,
        "Type for Adobe PDF files",
        null);

    /// <summary>
    /// Gets the details for the PowerPoint file type.
    /// </summary>
    internal static AddFileType PowerPoint => new(
        FileContentType.PowerPoint.Id,
        "Microsoft PowerPoint",
        FileContentType.PowerPoint.Type,
        [],
        FileContentType.PowerPoint.Extension,
        "Type for Microsoft PowerPoint files",
        null);

    /// <summary>
    /// Gets the details for the Text file type.
    /// </summary>
    internal static AddFileType Text => new(
        FileContentType.Text.Id,
        "Text file",
        FileContentType.Text.Type,
        [],
        FileContentType.Text.Extension,
        "Type for text files",
        null);

    /// <summary>
    /// Gets the details for the Word file type.
    /// </summary>
    internal static AddFileType Word => new(
        FileContentType.Word.Id,
        "Microsoft Word",
        FileContentType.Word.Type,
        [],
        FileContentType.Word.Extension,
        "Type for Microsoft Word files",
        null);

    /// <summary>
    /// Gets the details for the XML file type.
    /// </summary>
    internal static AddFileType Xml => new(
        FileContentType.Xml.Id,
        "Xml file",
        FileContentType.Xml.Type,
        [],
        FileContentType.Xml.Extension,
        "Type for XML files",
        null);
}