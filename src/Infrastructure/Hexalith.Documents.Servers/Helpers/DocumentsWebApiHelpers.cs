namespace Hexalith.Documents.Servers.Helpers;

using Hexalith.Documents.Domain.DocumentContainers;
using Hexalith.Documents.Domain.DocumentInformationExtractions;
using Hexalith.Documents.Domain.DocumentPartitions;
using Hexalith.Documents.Domain.Documents;
using Hexalith.Documents.Domain.DocumentTypes;
using Hexalith.Documents.Domain.FileTypes;
using Hexalith.Documents.Requests.DocumentContainers;
using Hexalith.Documents.Requests.DocumentInformationExtractions;
using Hexalith.Documents.Requests.DocumentPartitions;
using Hexalith.Documents.Requests.FileTypes;
using Hexalith.Documents.UI.Services.Documents.ViewModels;
using Hexalith.Documents.UI.Services.DocumentTypes.ViewModels;
using Hexalith.Documents.UI.Services.Helpers;
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
    /// <param name="applicationId">Name of the application.</param>
    /// <returns>IServiceCollection.</returns>
    /// <exception cref="ArgumentNullException">null.</exception>
    public static IServiceCollection AddDocumentsProjections(this IServiceCollection services, string applicationId)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentException.ThrowIfNullOrWhiteSpace(applicationId);
        _ = services.AddDocumentProjectionHandlers();
        _ = services.AddActorProjectionFactory<FileType>(applicationId);
        _ = services.AddActorProjectionFactory<FileTypeSummaryViewModel>(applicationId);
        _ = services.AddActorProjectionFactory<FileTypeDetailsViewModel>(applicationId);
        _ = services.AddActorProjectionFactory<DocumentType>(applicationId);
        _ = services.AddActorProjectionFactory<DocumentTypeSummaryViewModel>(applicationId);
        _ = services.AddActorProjectionFactory<DocumentTypeDetailsViewModel>(applicationId);
        _ = services.AddActorProjectionFactory<Document>(applicationId);
        _ = services.AddActorProjectionFactory<DocumentSummaryViewModel>(applicationId);
        _ = services.AddActorProjectionFactory<DocumentDetailsViewModel>(applicationId);
        _ = services.AddActorProjectionFactory<DocumentContainer>(applicationId);
        _ = services.AddActorProjectionFactory<DocumentContainerSummaryViewModel>(applicationId);
        _ = services.AddActorProjectionFactory<DocumentContainerDetailsViewModel>(applicationId);
        _ = services.AddActorProjectionFactory<DocumentPartition>(applicationId);
        _ = services.AddActorProjectionFactory<DocumentPartitionSummaryViewModel>(applicationId);
        _ = services.AddActorProjectionFactory<DocumentPartitionDetailsViewModel>(applicationId);
        _ = services.AddActorProjectionFactory<DocumentInformationExtraction>(applicationId);
        _ = services.AddActorProjectionFactory<DocumentInformationExtractionSummaryViewModel>(applicationId);
        _ = services.AddActorProjectionFactory<DocumentInformationExtractionDetailsViewModel>(applicationId);
        return services;
    }
}