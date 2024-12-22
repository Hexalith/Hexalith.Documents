namespace Hexalith.Documents.Projections.DocumentPartitions.Services;

using Hexalith.Documents.Commands.DocumentPartitions;
using Hexalith.Documents.Domain.ValueObjects;
using Hexalith.Documents.Requests.DocumentPartitions;

/// <summary>
/// Provides demo document partition data for testing and demonstration purposes.
/// This static class contains sample document partitions that can be used during development and testing.
/// </summary>
public static class DocumentPartitionQuickStartData
{
    /// <summary>
    /// Gets a collection of sample document partition details.
    /// </summary>
    /// <value>
    /// An enumerable collection of <see cref="DocumentPartitionDetailsViewModel"/> containing predefined document partitions.
    /// </value>
    public static IEnumerable<AddDocumentPartition> Data => [Default];

    /// <summary>
    /// Gets the details for the Excel document partition.
    /// </summary>
    internal static AddDocumentPartition Default => new(
        "Default",
        "Default storage in Azure",
        DocumentStorageType.AzureStorageContainer,
        "The default storage using Azure Storage Containers",
        string.Empty);
}