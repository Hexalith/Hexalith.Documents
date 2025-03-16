namespace Hexalith.Documents.Application.Services;

using Hexalith.Documents.Domain.ValueObjects;

/// <summary>
/// Provides an interface for reading files from various storage types.
/// </summary>
public interface IReadableFileProvider
{
    /// <summary>
    /// Opens a file asynchronously.
    /// </summary>
    /// <param name="storageType">The type of storage where the file is located.</param>
    /// <param name="connectionString">The connection string to the storage, if applicable.</param>
    /// <param name="path">The path to the file.</param>
    /// <param name="fileName">The name of the file.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the readable file.</returns>
    Task<IReadableFile> OpenFileAsync(
        DocumentStorageType storageType,
        string? connectionString,
        string path,
        string fileName,
        CancellationToken cancellationToken);
}