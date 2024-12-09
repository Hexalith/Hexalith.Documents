namespace Hexalith.Documents.UI.Pages.FileTypes.Services;

/// <summary>
/// Represents a demo implementation of the document query service that uses pre-defined data.
/// </summary>
/// <remarks>
/// This class extends the MemoryDocumentQueryService and initializes it with demo data,
/// making it useful for testing, demonstrations, or development scenarios where a
/// fully functional backend is not required.
/// </remarks>
public class DemoFileTypeQueryService()
    : MemoryFileTypeQueryService(DemoFileTypeData.Data)
{
}