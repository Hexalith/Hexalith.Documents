namespace Hexalith.Documents.Servers.Services;

using System;
using System.Threading;

using Hexalith.Documents.Application.Services;
using Hexalith.Documents.Servers.Configurations;
using Hexalith.Extensions.Configuration;

using Microsoft.Extensions.Options;

/// <summary>
/// Provides methods for file system storage operations.
/// </summary>
public class FileSystemStorage
{
    private readonly string _localStoragePath;

    /// <summary>
    /// Initializes a new instance of the <see cref="FileSystemStorage"/> class.
    /// </summary>
    /// <param name="environment">The web host environment.</param>
    /// <param name="options">The options containing the local storage settings.</param>
    public FileSystemStorage(IOptions<LocalStorageSettings> options)
    {
        ArgumentNullException.ThrowIfNull(options);
        SettingsException<LocalStorageSettings>.ThrowIfNullOrWhiteSpace(options.Value.Path, nameof(LocalStorageSettings.Path));
        _localStoragePath = options.Value.Path;
    }

    /// <summary>
    /// Creates a writable file asynchronously.
    /// </summary>
    /// <param name="path">The path where the file will be created.</param>
    /// <param name="fileName">The name of the file to be created.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the writable file.</returns>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "The Stream is in the Scope of the returned IWritableFile object")]
    public Task<IWritableFile> CreateFileAsync(string path, string fileName, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(path);
        ArgumentException.ThrowIfNullOrWhiteSpace(fileName);

        // Verify that the path value is relative.
        if (Path.IsPathRooted(path))
        {
            throw new ArgumentException("The path must be relative.", nameof(path));
        }

        // Verify that the file name is compliant with the Linux file system.
        if (fileName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
        {
            throw new ArgumentException("The file name contains invalid characters.", nameof(fileName));
        }

        string filePath = Path.Combine(_localStoragePath, path, fileName);
        if (File.Exists(filePath))
        {
            // Only create the file if it does not exist.
            throw new InvalidOperationException($"The file '{filePath}' already exists on the file system.");
        }

        string? folderPath = Path.GetDirectoryName(filePath);
        if (string.IsNullOrWhiteSpace(folderPath))
        {
            throw new InvalidOperationException($"The folder path for '{filePath}' is invalid.");
        }

        if (!Directory.Exists(folderPath))
        {
            // Create the directory if it does not exist.
            _ = Directory.CreateDirectory(folderPath);
        }

        cancellationToken.ThrowIfCancellationRequested();

        // Create the file.
        return Task.FromResult<IWritableFile>(new StorageFile(File.Create(filePath), filePath));
    }
}