﻿namespace Hexalith.Documents.Servers.Services;

using System;
using System.Threading;

using Hexalith.Documents.Application.Services;

/// <summary>
/// Provides methods to interact with Dropbox storage.
/// </summary>
public class DropboxStorage
{
    /// <summary>
    /// Creates a file asynchronously in Dropbox storage.
    /// </summary>
    /// <param name="connectionString">The connection string to Dropbox.</param>
    /// <param name="path">The path where the file will be created.</param>
    /// <param name="fileName">The name of the file to be created.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the writable file.</returns>
    public Task<IWritableFile> CreateFileAsync(string connectionString, string path, string fileName, CancellationToken cancellationToken) => throw new NotImplementedException();
}