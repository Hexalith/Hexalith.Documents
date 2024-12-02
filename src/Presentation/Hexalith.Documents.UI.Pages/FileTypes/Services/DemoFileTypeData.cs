namespace Hexalith.Documents.UI.Pages.FileTypes.Services;

using Hexalith.Documents.UI.Components.FileTypes.ViewModels;

/// <summary>
/// Provides demo document type data for testing and demonstration purposes.
/// This static class contains sample document types that can be used during development and testing.
/// </summary>
public static class DemoFileTypeData
{
    /// <summary>
    /// Gets a collection of sample document type details.
    /// </summary>
    /// <value>
    /// An enumerable collection of <see cref="FileTypeDetailsViewModel"/> containing predefined document types.
    /// </value>
    internal static IEnumerable<FileTypeDetailsViewModel> Data => [Excel, HTML, Markdown, Pdf, PowerPoint, Text, Word];

    /// <summary>
    /// Gets the details for the Excel document type.
    /// </summary>
    internal static FileTypeDetailsViewModel Excel => new(
        "Excel",
        "Microsoft Excel",
        "Type for Microsoft Excel files",
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
    /// Gets the details for the HTML document type.
    /// </summary>
    internal static FileTypeDetailsViewModel HTML => new(
        "HTML",
        "HTML file",
        "Type for HTML files",
        ["application/html", "text/html"],
        false);

    /// <summary>
    /// Gets the details for the Markdown document type.
    /// </summary>
    internal static FileTypeDetailsViewModel Markdown => new(
        "MD",
        "Markdown file",
        "Type for Markdown files",
        ["text/markdown", "text/x-markdown"],
        false);

    /// <summary>
    /// Gets the details for the PDF document type.
    /// </summary>
    internal static FileTypeDetailsViewModel Pdf => new(
        "PDF",
        "Adobe PDF",
        "Type for Adobe PDF files",
        ["application/pdf"],
        false);

    /// <summary>
    /// Gets the details for the PowerPoint document type.
    /// </summary>
    internal static FileTypeDetailsViewModel PowerPoint => new(
        "PowerPoint",
        "Microsoft PowerPoint",
        "Type for Microsoft PowerPoint files",
        [
            "application/vnd.ms-powerpoint.presentation.macroEnabled.12",
            "application/vnd.ms-powerpoint.slideshow",
            "application/vnd.ms-powerpoint.template",
            "application/vnd.openxmlformats-officedocument.presentationml.presentation",
            "application/mspowerpoint",
            "application/vnd.ms-powerpoint"],
        false);

    /// <summary>
    /// Gets the details for the Text document type.
    /// </summary>
    internal static FileTypeDetailsViewModel Text => new(
        "TXT",
        "Text file",
        "Type for text files",
        ["application/txt", "text/plain", "text/txt", "text/x-log"],
        false);

    /// <summary>
    /// Gets the details for the Word document type.
    /// </summary>
    internal static FileTypeDetailsViewModel Word => new(
        "Word",
        "Microsoft Word",
        "Type for Microsoft Word files",
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