namespace Hexalith.Documents.Projections.DocumentStorages.Services;

using Hexalith.Documents.Commands.DocumentStorages;
using Hexalith.Documents.Domain.ValueObjects;

/// <summary>
/// Provides demo document storage data for testing and demonstration purposes.
/// </summary>
/// <remarks>
/// This static class contains predefined document storage configurations that can be used
/// during development, testing, and initial system setup. It includes various storage types
/// such as local file system and Azure cloud storage examples.
/// </remarks>
public static class DocumentStorageQuickStartData
{
    /// <summary>
    /// Gets a collection of sample document storage configurations.
    /// </summary>
    /// <value>
    /// An enumerable collection of <see cref="AddDocumentStorage"/> commands containing
    /// predefined document storage configurations for local and cloud storage.
    /// </value>
    public static IEnumerable<AddDocumentStorage> Data => [Default, Temp, Azure];

    /// <summary>
    /// Gets a predefined Azure cloud storage configuration.
    /// </summary>
    /// <value>
    /// An <see cref="AddDocumentStorage"/> command for creating a cloud-based storage in Azure.
    /// </value>
    internal static AddDocumentStorage Azure => new(
            "Cloud",
            "Cloud storage in Azure",
            DocumentStorageType.AzureStorageContainer,
            "The cloud storage using Azure Storage Containers",
            "to be defined");

    /// <summary>
    /// Gets a predefined default storage configuration.
    /// </summary>
    /// <value>
    /// An <see cref="AddDocumentStorage"/> command for creating the default local file storage.
    /// </value>
    internal static AddDocumentStorage Default => new(
            "Default",
            "Default storage in Azure",
            DocumentStorageType.FileSystem,
            "The default storage using Azure Storage Containers",
            "C:\\storage-default");

    /// <summary>
    /// Gets a predefined temporary storage configuration.
    /// </summary>
    /// <value>
    /// An <see cref="AddDocumentStorage"/> command for creating a temporary local file storage.
    /// </value>
    internal static AddDocumentStorage Temp => new(
        "Temp",
        "Temporary local storage",
        DocumentStorageType.FileSystem,
        "Temporary storage for documents that are either waiting to be processed or are only needed for a short period.",
        "C:\\storage-temp");
}