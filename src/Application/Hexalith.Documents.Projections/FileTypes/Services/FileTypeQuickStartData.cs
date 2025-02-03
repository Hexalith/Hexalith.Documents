namespace Hexalith.Documents.Projections.FileTypes.Services;

using Hexalith.Documents.Commands.FileTypes;

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
    public static IEnumerable<AddFileType> Data => new[] { Excel, ExcelOld, HTML, Markdown, Pdf, PowerPoint, Text, Word, Json, Xml, Csv };

    /// <summary>
    /// Gets the details for the CSV file type.
    /// </summary>
    internal static AddFileType Csv => new(
        "CSV",
        "CSV file",
        "text/csv",
        [],
        "csv",
        [],
        null,
        null);

    /// <summary>
    /// Gets the details for the Excel file type.
    /// </summary>
    internal static AddFileType Excel => new(
        "Excel",
        "Microsoft Excel",
        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
        [],
        "xlsx",
        [],
        "Type for Microsoft Excel files",
        null);

    /// <summary>
    /// Gets the details for the old Excel file type.
    /// </summary>
    internal static AddFileType ExcelOld => new(
        "ExcelOld",
        "Microsoft Excel",
        "application/vnd.ms-excel",
        [],
        "xls",
        [],
        "Type for old Microsoft Excel files",
        null);

    /// <summary>
    /// Gets the details for the HTML file type.
    /// </summary>
    internal static AddFileType HTML => new(
        "Html",
        "HTML file",
        "text/html",
        ["application/html"],
        "html",
        ["htm"],
        "Type for HTML files",
        null);

    /// <summary>
    /// Gets the details for the Json file type.
    /// </summary>
    internal static AddFileType Json => new(
        "Json",
        "Json file",
        "application/json",
        [],
        "json",
        [],
        null,
        null);

    /// <summary>
    /// Gets the details for the Markdown file type.
    /// </summary>
    internal static AddFileType Markdown => new(
        "Markdown",
        "Markdown file",
        "text/markdown",
        [],
        "md",
        [],
        "Type for Markdown files",
        null);

    /// <summary>
    /// Gets the details for the PDF file type.
    /// </summary>
    internal static AddFileType Pdf => new(
        "Pdf",
        "Adobe PDF",
        "application/pdf",
        [],
        "pdf",
        [],
        "Type for Adobe PDF files",
        null);

    /// <summary>
    /// Gets the details for the PowerPoint file type.
    /// </summary>
    internal static AddFileType PowerPoint => new(
        "PowerPoint",
        "Microsoft PowerPoint",
        "application/vnd.openxmlformats-officedocument.presentationml.presentation",
        [],
        "pptx",
        [],
        "Type for Microsoft PowerPoint files",
        null);

    /// <summary>
    /// Gets the details for the Text file type.
    /// </summary>
    internal static AddFileType Text => new(
        "Text",
        "Text file",
        "text/plain",
        [],
        "txt",
        ["text"],
        "Type for text files",
        null);

    /// <summary>
    /// Gets the details for the Word file type.
    /// </summary>
    internal static AddFileType Word => new(
        "Word",
        "Microsoft Word",
        "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
        [],
        "docx",
        [],
        "Type for Microsoft Word files",
        null);

    /// <summary>
    /// Gets the details for the XML file type.
    /// </summary>
    internal static AddFileType Xml => new(
        "Xml",
        "Xml file",
        "application/xml",
        [],
        "xml",
        [],
        "Type for XML files",
        null);
}