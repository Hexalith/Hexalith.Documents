﻿namespace Hexalith.Documents.Servers.Services;

using System;
using System.Threading;

using Hexalith.Documents.Application.Services;

/// <summary>
/// Provides methods to interact with OneDrive storage.
/// </summary>
public class OneDriveStorage
{
    /// <summary>
    /// Creates a file in OneDrive storage asynchronously.
    /// </summary>
    /// <param name="connectionString">The connection string to OneDrive.</param>
    /// <param name="path">The path where the file will be created.</param>
    /// <param name="fileName">The name of the file to be created.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the writable file.</returns>
    public Task<IWritableFile> CreateFileAsync(string connectionString, string path, string fileName, CancellationToken cancellationToken) => throw new NotImplementedException();
}