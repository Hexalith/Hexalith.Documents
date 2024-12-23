namespace Hexalith.Documents.Servers.Services;

using System;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Documents.Application.Services;
using Hexalith.Documents.Domain.ValueObjects;

/// <summary>
/// Provides functionality to create writable files.
/// </summary>
public class WritableFileProvider : IWritableFileProvider
{
    /// <inheritdoc/>
    public Task<IWritableFile> CreateFileAsync(DocumentStorageType storageType, string connectionString, string path, string fileName, CancellationToken cancellationToken) => throw new NotImplementedException();
}