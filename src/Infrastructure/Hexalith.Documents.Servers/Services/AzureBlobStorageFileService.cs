//// <copyright file="AzureBlobStorageFileService.cs" company="ITANEO">
//// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
//// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//// </copyright>

// namespace Hexalith.Documents.Servers.Services;

// using System.Collections.Generic;
// using System.IO;
// using System.Threading.Tasks;

// using Hexalith.Documents.Application.Services;
// using Hexalith.Extensions.Configuration;

// using Microsoft.Extensions.Options;

///// <summary>
///// Provides file storage operations using Azure Blob Storage.
///// </summary>
///// <remarks>
///// This service implements the <see cref="IFileService"/> interface to provide file storage capabilities
///// using Azure Blob Storage as the underlying storage mechanism.
///// </remarks>
// public class AzureBlobStorageFileService : IFileService
// {
//    private readonly IOptions<AzureBlobFileServiceSettings> _settings;
//    private BlobServiceClient? _blobServiceClient;

// /// <summary>
//    /// Initializes a new instance of the <see cref="AzureBlobStorageFileService"/> class.
//    /// </summary>
//    /// <param name="settings">The Azure Blob Storage settings.</param>
//    public AzureBlobStorageFileService(IOptions<AzureBlobFileServiceSettings> settings)
//    {
//        ArgumentNullException.ThrowIfNull(settings);
//        _settings = settings;
//    }

// /// <summary>
//    /// Gets the BlobServiceClient instance, creating it if it doesn't exist.
//    /// </summary>
//    /// <returns>The initialized BlobServiceClient.</returns>
//    private BlobServiceClient BlobServiceClient => _blobServiceClient ??= CreateBlobServiceClient();

// /// <summary>
//    /// Downloads a file from Azure Blob Storage.
//    /// </summary>
//    /// <param name="containerName">The name of the container where the file is stored.</param>
//    /// <param name="fileId">The unique identifier of the file to download.</param>
//    /// <param name="writeStream">The stream where the file content will be written.</param>
//    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
//    /// <returns>A dictionary containing the blob's metadata tags.</returns>
//    /// <exception cref="ArgumentNullException">Thrown when <paramref name="fileId"/> or <paramref name="writeStream"/> is null.</exception>
//    /// <exception cref="ArgumentException">Thrown when <paramref name="fileId"/> is empty or consists only of white-space characters.</exception>
//    /// <exception cref="Azure.RequestFailedException">Thrown when a failure occurs while interacting with Azure Storage or the blob is not found.</exception>
//    public async Task<IDictionary<string, string>> DownloadAsync(string containerName, string fileId, Stream writeStream, CancellationToken cancellationToken)
//    {
//        ArgumentException.ThrowIfNullOrWhiteSpace(fileId);
//        ArgumentNullException.ThrowIfNull(writeStream);

// BlobContainerClient containerClient = BlobServiceClient.GetBlobContainerClient(containerName);
//        BlobClient blobClient = containerClient.GetBlobClient(fileId);

// _ = await blobClient.DownloadToAsync(writeStream, cancellationToken);

// Azure.Response<GetBlobTagResult> tags = await blobClient.GetTagsAsync(cancellationToken: cancellationToken);
//        return tags.Value.Tags;
//    }

// /// <inheritdoc/>
//    public async Task UploadAsync(
//       string containerId,
//       string documentTypeId,
//       string documentId,
//       string fileId,
//       Stream readStream,
//       IDictionary<string, string> tags,
//       CancellationToken cancellationToken)
//    {
//        ArgumentException.ThrowIfNullOrWhiteSpace(fileId);
//        ArgumentNullException.ThrowIfNull(readStream);

// BlobContainerClient containerClient = BlobServiceClient.GetBlobContainerClient(containerId);
//        _ = await containerClient.CreateIfNotExistsAsync(PublicAccessType.None, cancellationToken: cancellationToken);

// BlobClient blobClient = containerClient.GetBlobClient(fileId);
//        _ = await blobClient.UploadAsync(readStream, true, cancellationToken);

// if (tags != null && tags.Count > 0)
//        {
//            _ = await blobClient.SetTagsAsync(tags, cancellationToken: cancellationToken);
//        }
//    }

// /// <inheritdoc/>
//    public Task UploadAsync(string containerId, string documentTypeId, string documentId, string fileId, object data, IDictionary<string, string> tags, CancellationToken cancellationToken) => throw new NotImplementedException();

// private BlobServiceClient CreateBlobServiceClient()
//    {
//        if (_blobServiceClient is not null)
//        {
//            return _blobServiceClient;
//        }

// if (_settings.Value.ContainerUrl == null)
//        {
//            SettingsException<AzureBlobFileServiceSettings>.ThrowIfNullOrWhiteSpace(_settings.Value.ConnectionString);
//            _blobServiceClient = new BlobServiceClient(_settings.Value.ConnectionString);
//        }
//        else
//        {
//            SettingsException<AzureBlobFileServiceSettings>.ThrowIfNullOrWhiteSpace(_settings.Value.ApplicationSecret);
//            SettingsException<AzureBlobFileServiceSettings>.ThrowIfNullOrWhiteSpace(_settings.Value.ApplicationId);
//            SettingsException<AzureBlobFileServiceSettings>.ThrowIfNullOrWhiteSpace(_settings.Value.Tenant);
//            ClientSecretCredential credential = new(_settings.Value.Tenant, _settings.Value.ApplicationId, _settings.Value.ApplicationSecret);
//            _blobServiceClient = new BlobServiceClient(_settings.Value.ContainerUrl, credential);
//        }

// return _blobServiceClient;
//    }
// }