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
    public static IEnumerable<AddFileType> Data => [Excel, HTML, Markdown, Pdf, PowerPoint, Text, Word];

    /// <summary>
    /// Gets the details for the Excel file type.
    /// </summary>
    internal static AddFileType Excel => new(
        "Excel",
        "Microsoft Excel",
        "Type for Microsoft Excel files",
        null,
        [
            "application/vnd.ms-excel.sheet.binary.macroEnabled.12",
            "application/vnd.ms-excel.sheet.macroEnabled.12",
            "application/vnd.ms-excel.template",
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            "application/vnd.ms-excel",
            "application/x-excel",
            "application/x-msexcel"]);

    /// <summary>
    /// Gets the details for the HTML file type.
    /// </summary>
    internal static AddFileType HTML => new(
        "HTML",
        "HTML file",
        "Type for HTML files",
        null,
        ["application/html", "text/html"]);

    /// <summary>
    /// Gets the details for the Markdown file type.
    /// </summary>
    internal static AddFileType Markdown => new(
        "MD",
        "Markdown file",
        "Type for Markdown files",
        null,
        ["text/markdown", "text/x-markdown"]);

    /// <summary>
    /// Gets the details for the PDF file type.
    /// </summary>
    internal static AddFileType Pdf => new(
        "PDF",
        "Adobe PDF",
        "Type for Adobe PDF files",
        null,
        ["application/pdf"]);

    /// <summary>
    /// Gets the details for the PowerPoint file type.
    /// </summary>
    internal static AddFileType PowerPoint => new(
        "PowerPoint",
        "Microsoft PowerPoint",
        "Type for Microsoft PowerPoint files",
        null,
        [
            "application/vnd.ms-powerpoint.presentation.macroEnabled.12",
            "application/vnd.ms-powerpoint.slideshow",
            "application/vnd.ms-powerpoint.template",
            "application/vnd.openxmlformats-officedocument.presentationml.presentation",
            "application/mspowerpoint",
            "application/vnd.ms-powerpoint"]);

    /// <summary>
    /// Gets the details for the Text file type.
    /// </summary>
    internal static AddFileType Text => new(
        "TXT",
        "Text file",
        "Type for text files",
        null,
        ["application/txt", "text/plain", "text/txt", "text/x-log"]);

    /// <summary>
    /// Gets the details for the Word file type.
    /// </summary>
    internal static AddFileType Word => new(
        "Word",
        "Microsoft Word",
        "Type for Microsoft Word files",
        null,
        [
            "application/msword",
            "application/doc",
            "application/ms-doc",
            "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
            "application/msword-template",
            "application/vnd.ms-word.template",
            "application/vnd.ms-word.document.macroEnabled.12"]);
}