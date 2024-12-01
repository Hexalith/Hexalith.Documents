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
    internal static IEnumerable<FileTypeDetailsViewModel> Data => new[] { Excel, HTML, Markdown, Pdf, PowerPoint, Text, Word };

    /// <summary>
    /// Gets the details for the Excel document type.
    /// </summary>
    internal static FileTypeDetailsViewModel Excel => new(
        "Excel",
        "Microsoft Excel",
        "Type for Microsoft Excel files",
        false);

    /// <summary>
    /// Gets the details for the HTML document type.
    /// </summary>
    internal static FileTypeDetailsViewModel HTML => new(
        "HTML",
        "HTML file",
        "Type for HTML files",
        false);

    /// <summary>
    /// Gets the details for the Markdown document type.
    /// </summary>
    internal static FileTypeDetailsViewModel Markdown => new(
        "MD",
        "Markdown file",
        "Type for Markdown files",
        false);

    /// <summary>
    /// Gets the details for the PDF document type.
    /// </summary>
    internal static FileTypeDetailsViewModel Pdf => new(
        "PDF",
        "Adobe PDF",
        "Type for Adobe PDF files",
        false);

    /// <summary>
    /// Gets the details for the PowerPoint document type.
    /// </summary>
    internal static FileTypeDetailsViewModel PowerPoint => new(
        "PowerPoint",
        "Microsoft PowerPoint",
        "Type for Microsoft PowerPoint files",
        false);

    /// <summary>
    /// Gets the details for the Text document type.
    /// </summary>
    internal static FileTypeDetailsViewModel Text => new(
        "TXT",
        "Text file",
        "Type for text files",
        false);

    /// <summary>
    /// Gets the details for the Word document type.
    /// </summary>
    internal static FileTypeDetailsViewModel Word => new(
        "Word",
        "Microsoft Word",
        "Type for Microsoft Word files",
        false);
}