namespace Hexalith.Document.Domain;

/// <summary>
/// Helper class for the Document domain.
/// </summary>
[System.Diagnostics.CodeAnalysis.SuppressMessage(
    "Critical Code Smell",
    "S2339:Public constant members should not be used",
    Justification = "Const values are needed for attributes")]
public static class DocumentDomainHelper
{
    /// <summary>
    /// The name of the document aggregate.
    /// </summary>
    public const string DocumentAggregateName = "Document";

}