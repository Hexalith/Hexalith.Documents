namespace Hexalith.Documents.Servers.Services;

using System;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Documents.Application.Services;

/// <summary>
/// Provides methods to interact with Azure container storage.
/// </summary>
public class AzureContainerStorage
{
    /// <summary>
    /// Creates a file asynchronously in the specified Azure container.
    /// </summary>
    /// <param name="connectionString">The connection string to the Azure storage account.</param>
    /// <param name="path">The path within the container where the file will be created.</param>
    /// <param name="fileName">The name of the file to be created.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the writable file.</returns>
    public Task<IWritableFile> CreateFileAsync(string connectionString, string path, string fileName, CancellationToken cancellationToken)
        => throw new NotImplementedException();

    /// <summary>
    /// Reads a file asynchronously from the specified Azure container.
    /// </summary>
    /// <param name="connectionString">The connection string to the Azure storage account.</param>
    /// <param name="path">The path within the container where the file is located.</param>
    /// <param name="fileName">The name of the file to be read.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the readable file.</returns>
    public Task<IReadableFile> ReadFileAsync(string connectionString, string path, string fileName, CancellationToken cancellationToken)
        => throw new NotImplementedException();
}