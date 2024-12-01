namespace Hexalith.Documents.UI.Pages.Modules;

using Hexalith.UI.Components;
using Hexalith.UI.Components.Icons;

using Labels = Hexalith.Documents.UI.Pages.Resources.Modules.DocumentMenu;

/// <summary>
/// Represents the Document menu.
/// </summary>
public static class DocumentMenu
{
    /// <summary>
    /// Gets the menu information.
    /// </summary>
    public static MenuItemInformation Menu => new(
                    Labels.DocumentMenuItem,
                    string.Empty,
                    new IconInformation("DocumentDatabase", 20, IconStyle.Regular, IconSource.Fluent, IconLibraryName),
                    true,
                    10,
                    [
                        new MenuItemInformation(
                            Labels.DocumentMenuItem,
                            "Documents",
                            new IconInformation("DocumentBulletListMultiple", 20, IconStyle.Regular, IconSource.Fluent, IconLibraryName),
                            false,
                            30,
                            []),
                        new MenuItemInformation(
                            Labels.UploadMenuItem,
                            "Documents/Add",
                            new IconInformation("ArrowUpload", 20, IconStyle.Regular, IconSource.Fluent, IconLibraryName),
                            false,
                            20,
                            []),
                        new MenuItemInformation(
                            Labels.SetupMenuItem,
                            null,
                            new IconInformation("AppsSettings", 20, IconStyle.Regular, IconSource.Fluent, IconLibraryName),
                            false,
                            10,
                            [
                                new MenuItemInformation(
                                    Labels.DocumentTypeMenuItem,
                                    "Documents/DocumentType",
                                    new IconInformation("BookQuestionMarkRtl", 20, IconStyle.Regular, IconSource.Fluent, IconLibraryName),
                                    false,
                                    10,
                                    []),
                                new MenuItemInformation(
                                    Labels.FileTypeMenuItem,
                                    "Documents/FileType",
                                    new IconInformation("DocumentData", 20, IconStyle.Regular, IconSource.Fluent, IconLibraryName),
                                    false,
                                    10,
                                    []),
                                new MenuItemInformation(
                                    Labels.FileTextExtractionTypeMenuItem,
                                    "Documents/DocumentInformationExtraction",
                                    new IconInformation("ScanType", 20, IconStyle.Regular, IconSource.Fluent, IconLibraryName),
                                    false,
                                    10,
                                    []),
                                new MenuItemInformation(
                                    Labels.DocumentContainerTypeMenuItem,
                                    "Documents/DocumentContainer",
                                    new IconInformation("DocumentFolder", 20, IconStyle.Regular, IconSource.Fluent, IconLibraryName),
                                    false,
                                    10,
                                    []),
                                ]),
                    ]);

    private static string IconLibraryName
        => typeof(DocumentMenu).Assembly?.FullName
            ?? throw new InvalidOperationException("Menu Assembly not found");
}