namespace Hexalith.Documents.Application;

/// <summary>
/// Defines the policies for document security within the application.
/// </summary>
public static class DocumentPolicies
{
    /// <summary>
    /// Policy for users who can contribute to documents.
    /// </summary>
    public const string Contributors = nameof(Documents) + nameof(Contributors);

    /// <summary>
    /// Policy for users who own documents.
    /// </summary>
    public const string Owners = nameof(Documents) + nameof(Owners);

    /// <summary>
    /// Policy for users who can read documents.
    /// </summary>
    public const string Readers = nameof(Documents) + nameof(Readers);
}