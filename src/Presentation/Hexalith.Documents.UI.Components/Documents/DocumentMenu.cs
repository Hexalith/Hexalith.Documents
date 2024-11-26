namespace Hexalith.Documents.UI.Components.Documents;

using Hexalith.UI.Components;
using Hexalith.UI.Components.Icons;

using Labels = Hexalith.Documents.Shared.Resources.Modules.DocumentMenu;

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
                            "Document/Document",
                            new IconInformation("DocumentBulletListMultiple", 20, IconStyle.Regular, IconSource.Fluent, IconLibraryName),
                            false,
                            10,
                            []),
                    ]);

    private static string IconLibraryName
        => typeof(DocumentMenu).Assembly?.FullName
            ?? throw new InvalidOperationException("Menu Assembly not found");
}