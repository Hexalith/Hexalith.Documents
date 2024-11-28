namespace Hexalith.Documents.Application;

using Hexalith.Application;

/// <summary>
/// Defines the roles for document security within the application.
/// </summary>
public static class DocumentRoles
{
    /// <summary>
    /// Role for users who can contribute to documents.
    /// </summary>
    public const string Contributor = nameof(Documents) + nameof(Contributor);

    /// <summary>
    /// Role for users who own documents.
    /// </summary>
    public const string Owner = nameof(Documents) + nameof(Owner);

    /// <summary>
    /// Role for users who can read documents.
    /// </summary>
    public const string Reader = nameof(Documents) + nameof(Reader);

    /// <summary>
    /// Gets the roles for users who can contribute to documents.
    /// </summary>
    public static string Contributors => $"{Owners},{Contributor}";

    /// <summary>
    /// Gets the roles for users who own documents.
    /// </summary>
    public static string Owners => $"{Owner},{ApplicationRoles.GlobalAdministrator}";

    /// <summary>
    /// Gets the roles for users who can read documents.
    /// </summary>
    public static string Readers => $"{Contributors},{Reader}";
}