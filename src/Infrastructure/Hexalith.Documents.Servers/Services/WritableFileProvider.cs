namespace Hexalith.Documents.Servers.Services;

using System;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Documents.Application.Services;
using Hexalith.Documents.Domain.ValueObjects;

using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Provides functionality to create writable files.
/// </summary>
public class WritableFileProvider : IWritableFileProvider, IReadableFileProvider
{
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="WritableFileProvider"/> class.
    /// </summary>
    /// <param name="serviceProvider">The service provider instance used to resolve dependencies.</param>
    public WritableFileProvider(IServiceProvider serviceProvider)
    {
        ArgumentNullException.ThrowIfNull(serviceProvider);
        _serviceProvider = serviceProvider;
    }

    /// <inheritdoc/>
    public async Task<IWritableFile> CreateFileAsync(DocumentStorageType storageType, string? connectionString, string path, string fileName, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(connectionString);
        ArgumentNullException.ThrowIfNull(path);
        ArgumentNullException.ThrowIfNull(fileName);
        return storageType switch
        {
            DocumentStorageType.AzureStorageContainer => await _serviceProvider.GetRequiredService<AzureContainerStorage>().CreateFileAsync(connectionString, path, fileName, cancellationToken).ConfigureAwait(false),
            DocumentStorageType.OneDrive => await _serviceProvider.GetRequiredService<OneDriveStorage>().CreateFileAsync(connectionString, path, fileName, cancellationToken).ConfigureAwait(false),
            DocumentStorageType.FileSystem => await _serviceProvider.GetRequiredService<FileSystemStorage>().CreateFileAsync(connectionString, path, fileName, cancellationToken).ConfigureAwait(false),
            DocumentStorageType.Dropbox => await _serviceProvider.GetRequiredService<DropboxStorage>().CreateFileAsync(connectionString, path, fileName, cancellationToken).ConfigureAwait(false),
            DocumentStorageType.GoogleDrive => await _serviceProvider.GetRequiredService<GoogleDriveStorage>().CreateFileAsync(connectionString, path, fileName, cancellationToken).ConfigureAwait(false),
            DocumentStorageType.AwsS3Bucket => await _serviceProvider.GetRequiredService<AwsS3BucketStorage>().CreateFileAsync(connectionString, path, fileName, cancellationToken).ConfigureAwait(false),
            DocumentStorageType.Sharepoint => await _serviceProvider.GetRequiredService<SharepointStorage>().CreateFileAsync(connectionString, path, fileName, cancellationToken).ConfigureAwait(false),
            _ => throw new NotSupportedException($"Storage type {storageType} is not supported."),
        };
    }

    /// <summary>
    /// Opens a readable file.
    /// </summary>
    /// <param name="storageType">The type of storage where the file is located.</param>
    /// <param name="connectionString">The connection string to the storage.</param>
    /// <param name="path">The path to the file.</param>
    /// <param name="fileName">The name of the file.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the readable file.</returns>
    /// <exception cref="ArgumentNullException">Thrown when any of the required parameters are null.</exception>
    /// <exception cref="NotSupportedException">Thrown when the specified storage type is not supported.</exception>
    public async Task<IReadableFile> OpenFileAsync(DocumentStorageType storageType, string? connectionString, string path, string fileName, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(connectionString);
        ArgumentNullException.ThrowIfNull(path);
        ArgumentNullException.ThrowIfNull(fileName);
        return storageType switch
        {
            DocumentStorageType.AzureStorageContainer => await _serviceProvider.GetRequiredService<AzureContainerStorage>().ReadFileAsync(connectionString, path, fileName, cancellationToken).ConfigureAwait(false),
            DocumentStorageType.OneDrive => await _serviceProvider.GetRequiredService<OneDriveStorage>().ReadFileAsync(connectionString, path, fileName, cancellationToken).ConfigureAwait(false),
            DocumentStorageType.FileSystem => await _serviceProvider.GetRequiredService<FileSystemStorage>().ReadFileAsync(connectionString, path, fileName, cancellationToken).ConfigureAwait(false),
            DocumentStorageType.Dropbox => await _serviceProvider.GetRequiredService<DropboxStorage>().ReadFileAsync(connectionString, path, fileName, cancellationToken).ConfigureAwait(false),
            DocumentStorageType.GoogleDrive => await _serviceProvider.GetRequiredService<GoogleDriveStorage>().ReadFileAsync(connectionString, path, fileName, cancellationToken).ConfigureAwait(false),
            DocumentStorageType.AwsS3Bucket => await _serviceProvider.GetRequiredService<AwsS3BucketStorage>().ReadFileAsync(connectionString, path, fileName, cancellationToken).ConfigureAwait(false),
            DocumentStorageType.Sharepoint => await _serviceProvider.GetRequiredService<SharepointStorage>().ReadFileAsync(connectionString, path, fileName, cancellationToken).ConfigureAwait(false),
            _ => throw new NotSupportedException($"Storage type {storageType} is not supported."),
        };
    }
}