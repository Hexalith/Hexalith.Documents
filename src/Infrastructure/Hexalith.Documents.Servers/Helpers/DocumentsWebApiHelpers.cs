namespace Hexalith.Documents.Servers.Helpers;

using Hexalith.Documents.Application.Services;
using Hexalith.Documents.Domain.DataExports;
using Hexalith.Documents.Domain.DocumentContainers;
using Hexalith.Documents.Domain.DocumentInformationExtractions;
using Hexalith.Documents.Domain.DocumentPartitions;
using Hexalith.Documents.Domain.Documents;
using Hexalith.Documents.Domain.DocumentTypes;
using Hexalith.Documents.Domain.FileTypes;
using Hexalith.Documents.Projections.Helpers;
using Hexalith.Documents.Requests.DataExports;
using Hexalith.Documents.Requests.DocumentContainers;
using Hexalith.Documents.Requests.DocumentInformationExtractions;
using Hexalith.Documents.Requests.DocumentPartitions;
using Hexalith.Documents.Requests.Documents;
using Hexalith.Documents.Requests.DocumentTypes;
using Hexalith.Documents.Requests.FileTypes;
using Hexalith.Documents.Servers.Services;
using Hexalith.Infrastructure.DaprRuntime.Helpers;

using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Class PartiesWebApiHelpers.
/// </summary>
public static class DocumentsWebApiHelpers
{
    /// <summary>
    /// Adds the customer projections.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <returns>IServiceCollection.</returns>
    /// <exception cref="ArgumentNullException">null.</exception>
    public static IServiceCollection AddDocumentsProjectionActorFactories(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);
        _ = services.AddTransient<IWritableFileProvider, WritableFileProvider>();
        _ = services.AddDocumentProjections();
        _ = services.AddActorProjectionFactory<DataExport>();
        _ = services.AddActorProjectionFactory<DataExportSummaryViewModel>();
        _ = services.AddActorProjectionFactory<DataExportDetailsViewModel>();
        _ = services.AddActorProjectionFactory<FileType>();
        _ = services.AddActorProjectionFactory<FileTypeSummaryViewModel>();
        _ = services.AddActorProjectionFactory<FileTypeDetailsViewModel>();
        _ = services.AddActorProjectionFactory<DocumentType>();
        _ = services.AddActorProjectionFactory<DocumentTypeSummaryViewModel>();
        _ = services.AddActorProjectionFactory<DocumentTypeDetailsViewModel>();
        _ = services.AddActorProjectionFactory<Document>();
        _ = services.AddActorProjectionFactory<DocumentSummaryViewModel>();
        _ = services.AddActorProjectionFactory<DocumentDetailsViewModel>();
        _ = services.AddActorProjectionFactory<DocumentContainer>();
        _ = services.AddActorProjectionFactory<DocumentContainerSummaryViewModel>();
        _ = services.AddActorProjectionFactory<DocumentContainerDetailsViewModel>();
        _ = services.AddActorProjectionFactory<DocumentPartition>();
        _ = services.AddActorProjectionFactory<DocumentPartitionSummaryViewModel>();
        _ = services.AddActorProjectionFactory<DocumentPartitionDetailsViewModel>();
        _ = services.AddActorProjectionFactory<DocumentInformationExtraction>();
        _ = services.AddActorProjectionFactory<DocumentInformationExtractionSummaryViewModel>();
        _ = services.AddActorProjectionFactory<DocumentInformationExtractionDetailsViewModel>();
        return services;
    }
}