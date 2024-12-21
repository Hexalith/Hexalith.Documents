namespace Hexalith.Documents.Application.DataExports;

using Hexalith.Documents.Application.Services;
using Hexalith.Documents.Domain.ValueObjects;

public interface IWritableFileProvider
{
    Task<IWritableFile> CreateFileAsync(DocumentStorageType storageType, string connectionString, string path, string fileName, CancellationToken cancellationToken);
}