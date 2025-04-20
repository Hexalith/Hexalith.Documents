// <copyright file="DocumentStorageType.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.ValueObjects;

using System.Text.Json.Serialization;

/// <summary>
/// Specifies the type of storage for a document.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum DocumentStorageType
{
    /// <summary>
    /// The document is stored as a local file.
    /// </summary>
    FileSystem = 0,

    /// <summary>
    /// The document is stored in an Azure Storage Container.
    /// </summary>
    AzureStorageContainer = 1,

    /// <summary>
    /// The document is stored in OneDrive.
    /// </summary>
    OneDrive = 2,

    /// <summary>
    /// The document is stored in Google Drive.
    /// </summary>
    GoogleDrive = 3,

    /// <summary>
    /// The document is stored in Dropbox.
    /// </summary>
    Dropbox = 4,

    /// <summary>
    /// The document is stored in an AWS S3 Bucket.
    /// </summary>
    AwsS3Bucket = 5,

    /// <summary>
    /// The document is stored in SharePoint.
    /// </summary>
    Sharepoint = 6,
}