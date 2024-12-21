namespace Hexalith.Documents.Domain.ValueObjects;

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
    LocalFile,

    /// <summary>
    /// The document is stored in an Azure Storage Container.
    /// </summary>
    AzureStorageContainer,

    /// <summary>
    /// The document is stored in OneDrive.
    /// </summary>
    OneDrive,

    /// <summary>
    /// The document is stored in Google Drive.
    /// </summary>
    GoogleDrive,

    /// <summary>
    /// The document is stored in Dropbox.
    /// </summary>
    Dropbox,

    /// <summary>
    /// The document is stored in an AWS S3 Bucket.
    /// </summary>
    AwsS3Bucket,

    /// <summary>
    /// The document is stored in SharePoint.
    /// </summary>
    Sharepoint,
}