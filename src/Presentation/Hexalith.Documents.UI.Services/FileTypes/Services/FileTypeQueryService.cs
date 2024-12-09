namespace Hexalith.Documents.UI.Services.FileTypes.Services;

using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Requests;
using Hexalith.Documents.Requests.FileTypes;
using Hexalith.UI.Components.ViewModels;

/// <summary>
/// Provides query operations for file types.
/// </summary>
public class FileTypeQueryService : IFileTypeQueryService
{
    private readonly IRequestService _requestService;

    /// <summary>
    /// Initializes a new instance of the <see cref="FileTypeQueryService"/> class.
    /// </summary>
    /// <param name="requestService">The request service.</param>
    public FileTypeQueryService(IRequestService requestService)
    {
        ArgumentNullException.ThrowIfNull(requestService);
        _requestService = requestService;
    }

    /// <inheritdoc/>
    public async Task<FileTypeDetailsViewModel> GetDetailsAsync(ClaimsPrincipal user, string id, CancellationToken cancellationToken)
        => CheckValidResult((await _requestService.SubmitAsync(user, new GetFileTypeDetails(id), cancellationToken)
            .ConfigureAwait(false)).Result);

    /// <inheritdoc/>
    public async Task<IdDescription> GetIdDescriptionAsync(ClaimsPrincipal user, string id, CancellationToken cancellationToken)
        => CheckValidResult((await _requestService.SubmitAsync(user, new GetFileTypeIdDescription(id), cancellationToken)
            .ConfigureAwait(false)).Result);

    /// <inheritdoc/>
    public Task<IEnumerable<IdDescription>> GetIdDescriptionsAsync(ClaimsPrincipal user, int skip, int take, CancellationToken cancellationToken) => throw new NotImplementedException();

    /// <inheritdoc/>
    public Task<IEnumerable<FileTypeSummaryViewModel>> GetSummariesAsync(ClaimsPrincipal user, int skip, int take, CancellationToken cancellationToken) => throw new NotImplementedException();

    /// <inheritdoc/>
    public Task<IEnumerable<IdDescription>> SearchIdDescriptionsAsync(ClaimsPrincipal user, string searchText, int skip, int count, CancellationToken cancellationToken) => throw new NotImplementedException();

    /// <inheritdoc/>
    public Task<IEnumerable<FileTypeSummaryViewModel>> SearchSummariesAsync(ClaimsPrincipal user, string searchText, CancellationToken cancellationToken) => throw new NotImplementedException();

    private static TResult CheckValidResult<TResult>(TResult? result)
        => result ?? throw new InvalidOperationException("The request result is null or empty in file type query service");
}