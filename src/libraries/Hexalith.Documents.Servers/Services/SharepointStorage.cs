﻿namespace Hexalith.Documents.Servers.Services;

using System;
using System.Threading;

using Hexalith.Documents.Application.Services;

/// <summary>
/// Provides methods to interact with SharePoint storage.
/// </summary>
public class SharepointStorage
{
    /// <summary>
    /// Creates a file asynchronously in the SharePoint storage.
    /// </summary>
    /// <param name="connectionString">The connection string to the SharePoint storage.</param>
    /// <param name="path">The path where the file will be created.</param>
    /// <param name="fileName">The name of the file to be created.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the writable file.</returns>
    public Task<IWritableFile> CreateFileAsync(string connectionString, string path, string fileName, CancellationToken cancellationToken) => throw new NotImplementedException();

    /// <summary>
    /// Reads a file asynchronously from the SharePoint storage.
    /// </summary>
    /// <param name="connectionString">The connection string to the SharePoint storage.</param>
    /// <param name="path">The path where the file is located.</param>
    /// <param name="fileName">The name of the file to be read.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the readable file.</returns>
    public Task<IReadableFile> ReadFileAsync(string connectionString, string path, string fileName, CancellationToken cancellationToken) => throw new NotImplementedException();
}