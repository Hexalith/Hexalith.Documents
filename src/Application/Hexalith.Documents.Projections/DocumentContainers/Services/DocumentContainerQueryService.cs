namespace Hexalith.Documents.UI.Services.DocumentContainers.Services;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Requests;
using Hexalith.Application.Services;
using Hexalith.Documents.Projections.DocumentContainers.Services;
using Hexalith.Documents.Requests.DocumentContainers;

using Microsoft.Extensions.Logging;

/// <summary>
/// Provides query operations for document containers.
/// </summary>
public partial class DocumentContainerQueryService : IDocumentContainerQueryService
{
    private readonly ILogger<DocumentContainerQueryService> _logger;
    private readonly IRequestService _requestService;

    /// <summary>
    /// Initializes a new instance of the <see cref="DocumentContainerQueryService"/> class.
    /// </summary>
    /// <param name="requestService">The request service.</param>
    /// <param name="logger">The logger instance.</param>
    public DocumentContainerQueryService(IRequestService requestService, ILogger<DocumentContainerQueryService> logger)
    {
        ArgumentNullException.ThrowIfNull(requestService);
        ArgumentNullException.ThrowIfNull(logger);
        _requestService = requestService;
        _logger = logger;
    }

    [LoggerMessage(EventId = 1, Level = LogLevel.Information, Message = "GetSummariesAsync: received {Count} summaries.")]
    public static partial void LogGetSummaries(ILogger logger, int count);

    /// <inheritdoc/>
    public async Task<DocumentContainerDetailsViewModel> GetDetailsAsync(ClaimsPrincipal user, string id, CancellationToken cancellationToken)
        => CheckValidResult((await _requestService.SubmitAsync(user, new GetDocumentContainerDetails(id), cancellationToken)
            .ConfigureAwait(false)).Result);

    /// <inheritdoc/>
    public async Task<IdDescription> GetIdDescriptionAsync(ClaimsPrincipal user, string id, CancellationToken cancellationToken)
        => CheckValidResult((await _requestService.SubmitAsync(user, new GetDocumentContainerIdDescription(id), cancellationToken)
            .ConfigureAwait(false)).Result);

    /// <inheritdoc/>
    public async Task<IEnumerable<IdDescription>> GetIdDescriptionsAsync(ClaimsPrincipal user, int skip, int take, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(user);
        IEnumerable<string> ids = CheckValidResult((await _requestService.SubmitAsync(user, new GetDocumentContainerIds(skip, take), cancellationToken)
            .ConfigureAwait(false)).Result);
        List<Task<IdDescription>> tasks = [];
        foreach (string id in ids)
        {
            tasks.Add(GetIdDescriptionAsync(user, id, cancellationToken));
        }

        return await Task.WhenAll(tasks).ConfigureAwait(false);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<DocumentContainerSummaryViewModel>> GetSummariesAsync(ClaimsPrincipal user, int skip, int take, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(user);

        GetDocumentContainerSummaries request = await _requestService.SubmitAsync(user, new GetDocumentContainerSummaries(skip, take), cancellationToken).ConfigureAwait(false);
        _ = CheckValidResult(request);
        LogGetSummaries(_logger, request.Result.Count());
        return request.Result;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<IdDescription>> SearchIdDescriptionsAsync(ClaimsPrincipal user, string searchText, int skip, int take, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(user);
        IEnumerable<IdDescription> data = await GetIdDescriptionsAsync(user, skip, take, cancellationToken).ConfigureAwait(false);
        if (!string.IsNullOrWhiteSpace(searchText))
        {
            data = data.Where(d =>
                d.Description.Contains(searchText, StringComparison.CurrentCultureIgnoreCase) ||
                d.Id.Contains(searchText, StringComparison.CurrentCultureIgnoreCase));
        }

        if (skip > 0)
        {
            data = data.Skip(skip);
        }

        if (take > 0)
        {
            data = data.Take(take);
        }

        return data;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<DocumentContainerSummaryViewModel>> SearchSummariesAsync(ClaimsPrincipal user, string searchText, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(user);
        IEnumerable<DocumentContainerSummaryViewModel> data = await GetSummariesAsync(user, 0, 0, cancellationToken).ConfigureAwait(false);
        if (!string.IsNullOrWhiteSpace(searchText))
        {
            data = data.Where(d =>
                d.Id.Contains(searchText, StringComparison.CurrentCultureIgnoreCase) ||
                d.Name.Contains(searchText, StringComparison.CurrentCultureIgnoreCase));
        }

        return data;
    }

    private static TResult CheckValidResult<TResult>([NotNull] TResult? result)
        => result ?? throw new InvalidOperationException("The request result is null or empty in document container query service");
}