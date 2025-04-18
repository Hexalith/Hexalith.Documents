namespace Hexalith.Documents;

/// <summary>
/// Provides constant definitions for aggregate names in the Document domain.
/// This class serves as a central reference point for all document-related aggregate identifiers.
/// </summary>
/// <remarks>
/// <para>
/// The Document domain is structured around several key aggregates, each handling specific aspects of document management:
/// - Data Management: Handles general document data operations and maintenance
/// - Document: Core document entity management
/// - Document Container: Groups and organizes documents
/// - Information Extraction: Processes and extracts data from documents
/// - Storage: Manages physical document storage
/// - Document Type: Defines and manages document classifications
/// - File Type: Handles file format specifications.
/// </para>
/// <para>
/// These aggregate names are used throughout the system for:
/// - Event topic routing
/// - Message bus configuration
/// - Aggregate identification
/// - Domain event correlation.
/// </para>
/// </remarks>
public static class DocumentDomainHelper
{
    /// <summary>
    /// The aggregate name for data management operations.
    /// Used for handling document data exports, imports, and general data maintenance operations.
    /// </summary>
    public const string DataManagementAggregateName = "DataManagement";

    /// <summary>
    /// The aggregate name for core document operations.
    /// Represents the main document entity and handles basic document lifecycle operations.
    /// </summary>
    public const string DocumentAggregateName = "Document";

    /// <summary>
    /// The aggregate name for document container operations.
    /// Manages document grouping, hierarchical organization, and container-level operations.
    /// </summary>
    public const string DocumentContainerAggregateName = "DocumentContainer";

    /// <summary>
    /// The aggregate name for document information extraction operations.
    /// Handles the extraction, processing, and analysis of data from document content.
    /// </summary>
    public const string DocumentInformationExtractionAggregateName = "DocumentInformationExtraction";

    /// <summary>
    /// The aggregate name for document storage operations.
    /// Manages physical storage, retrieval, and archiving of documents.
    /// </summary>
    public const string DocumentStorageAggregateName = "DocumentStorage";

    /// <summary>
    /// The aggregate name for document type operations.
    /// Handles document classification, type definitions, and related metadata.
    /// </summary>
    public const string DocumentTypeAggregateName = "DocumentType";

    /// <summary>
    /// The aggregate name for file type operations.
    /// Manages file format definitions, validations, and format-specific processing rules.
    /// </summary>
    public const string FileTypeAggregateName = "FileType";
}