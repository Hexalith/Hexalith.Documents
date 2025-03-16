namespace Hexalith.Documents.Application.Services;

using Hexalith.Documents.Domain.ValueObjects;

/// <summary>
/// Provides an interface for creating writable files.
/// </summary>
public interface IWritableFileProvider
{
    /// <summary>
    /// Creates a writable file asynchronously.
    /// </summary>
    /// <param name="storageType">The type of storage to use.</param>
    /// <param name="connectionString">The connection string for the storage.</param>
    /// <param name="path">The path where the file will be created.</param>
    /// <param name="fileName">The name of the file to be created.</param>
    /// <param name="tags">The tags associated with the file.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the writable file.</returns>
    Task<IWritableFile> CreateFileAsync(
        DocumentStorageType storageType,
        string? connectionString,
        string path,
        string fileName,
        IEnumerable<(string Key, string? Value)> tags,
        CancellationToken cancellationToken);
}