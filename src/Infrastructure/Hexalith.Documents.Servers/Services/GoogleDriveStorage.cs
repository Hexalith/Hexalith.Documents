﻿namespace Hexalith.Documents.Servers.Services;

using System;
using System.Threading;

using Hexalith.Documents.Application.Services;

/// <summary>
/// Provides methods to interact with Google Drive storage.
/// </summary>
public class GoogleDriveStorage
{
    /// <summary>
    /// Creates a file asynchronously in Google Drive.
    /// </summary>
    /// <param name="connectionString">The connection string to Google Drive.</param>
    /// <param name="path">The path where the file will be created.</param>
    /// <param name="fileName">The name of the file to be created.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the writable file.</returns>
    public Task<IWritableFile> CreateFileAsync(string connectionString, string path, string fileName, CancellationToken cancellationToken) => throw new NotImplementedException();
}