namespace Hexalith.Documents.SharedAssets.Modules;

using Hexalith.UI.Components;
using Hexalith.UI.Components.Icons;

using Labels = Shared.Resources.Modules.DocumentMenu;

/// <summary>
/// Represents the Document menu.
/// </summary>
public static class DocumentMenu
{
    private const string _iconLibraryName = $"{nameof(Document)}.{nameof(Shared)}";

    /// <summary>
    /// Gets the menu information.
    /// </summary>
    public static MenuItemInformation Menu => new(
                    Labels.DocumentMenuItem,
                    string.Empty,
                    new IconInformation("DocumentDatabase", 20, IconStyle.Regular, IconSource.Fluent, _iconLibraryName),
                    true,
                    10,
                    [
                        new MenuItemInformation(
                            Labels.DocumentMenuItem,
                            "Document/Document",
                            new IconInformation("DocumentBulletListMultiple", 20, IconStyle.Regular, IconSource.Fluent, _iconLibraryName),
                            false,
                            10,
                            []),
                    ]);
}