namespace Hexalith.Documents.Projections.DataExports.Services;

using Hexalith.Documents.Requests.DataExports;

/// <summary>
/// Provides demo data export data for testing and demonstration purposes.
/// This static class contains sample data exports that can be used during development and testing.
/// </summary>
public static class DataExportQuickStartData
{
    /// <summary>
    /// Gets a collection of sample data export details.
    /// </summary>
    /// <value>
    /// An enumerable collection of <see cref="DataExportDetailsViewModel"/> containing predefined data exports.
    /// </value>
    public static IEnumerable<DataExportDetailsViewModel> Data => [Excel, HTML, Markdown, Pdf, PowerPoint, Text, Word];

    /// <summary>
    /// Gets the details for the Excel data export.
    /// </summary>
    internal static DataExportDetailsViewModel Excel => new(
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
            "application/x-msexcel"],
        false);

    /// <summary>
    /// Gets the details for the HTML data export.
    /// </summary>
    internal static DataExportDetailsViewModel HTML => new(
        "HTML",
        "HTML file",
        "Type for HTML files",
        null,
        ["application/html", "text/html"],
        false);

    /// <summary>
    /// Gets the details for the Markdown data export.
    /// </summary>
    internal static DataExportDetailsViewModel Markdown => new(
        "MD",
        "Markdown file",
        "Type for Markdown files",
        null,
        ["text/markdown", "text/x-markdown"],
        false);

    /// <summary>
    /// Gets the details for the PDF data export.
    /// </summary>
    internal static DataExportDetailsViewModel Pdf => new(
        "PDF",
        "Adobe PDF",
        "Type for Adobe PDF files",
        null,
        ["application/pdf"],
        false);

    /// <summary>
    /// Gets the details for the PowerPoint data export.
    /// </summary>
    internal static DataExportDetailsViewModel PowerPoint => new(
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
            "application/vnd.ms-powerpoint"],
        false);

    /// <summary>
    /// Gets the details for the Text data export.
    /// </summary>
    internal static DataExportDetailsViewModel Text => new(
        "TXT",
        "Text file",
        "Type for text files",
        null,
        ["application/txt", "text/plain", "text/txt", "text/x-log"],
        false);

    /// <summary>
    /// Gets the details for the Word data export.
    /// </summary>
    internal static DataExportDetailsViewModel Word => new(
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
            "application/vnd.ms-word.document.macroEnabled.12"],
        false);
}