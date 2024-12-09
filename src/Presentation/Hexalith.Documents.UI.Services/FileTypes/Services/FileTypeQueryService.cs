namespace Hexalith.Documents.UI.Services.FileTypes.Services;

using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Documents.UI.Services.FileTypes.ViewModels;
using Hexalith.UI.Components.ViewModels;

/// <summary>
/// Provides query operations for file types.
/// </summary>
public class FileTypeQueryService : IFileTypeQueryService
{
    /// <inheritdoc/>
    public Task<FileTypeDetailsViewModel> GetDetailsAsync(ClaimsPrincipal user, string id, CancellationToken cancellationToken) => throw new NotImplementedException();

    /// <inheritdoc/>
    public Task<IdDescription> GetIdDescriptionAsync(ClaimsPrincipal user, string id, CancellationToken cancellationToken) => throw new NotImplementedException();

    /// <inheritdoc/>
    public Task<IEnumerable<IdDescription>> GetIdDescriptionsAsync(ClaimsPrincipal user, int skip, int take, CancellationToken cancellationToken) => throw new NotImplementedException();

    /// <inheritdoc/>
    public Task<IEnumerable<FileTypeSummaryViewModel>> GetSummariesAsync(ClaimsPrincipal user, int skip, int take, CancellationToken cancellationToken) => throw new NotImplementedException();

    /// <inheritdoc/>
    public Task<IEnumerable<IdDescription>> SearchIdDescriptionsAsync(ClaimsPrincipal user, string searchText, int skip, int count, CancellationToken cancellationToken) => throw new NotImplementedException();

    /// <inheritdoc/>
    public Task<IEnumerable<FileTypeSummaryViewModel>> SearchSummariesAsync(ClaimsPrincipal user, string searchText, CancellationToken cancellationToken) => throw new NotImplementedException();
}