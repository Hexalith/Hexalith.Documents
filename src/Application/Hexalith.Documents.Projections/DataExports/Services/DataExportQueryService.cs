namespace Hexalith.Documents.UI.Services.DataExports.Services;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Requests;
using Hexalith.Application.Services;
using Hexalith.Documents.Projections.DataExports.Services;
using Hexalith.Documents.Requests.DataExports;

using Microsoft.Extensions.Logging;

/// <summary>
/// Provides query operations for data exports.
/// </summary>
public partial class DataExportQueryService : IDataExportQueryService
{
    private readonly ILogger<DataExportQueryService> _logger;
    private readonly IRequestService _requestService;

    /// <summary>
    /// Initializes a new instance of the <see cref="DataExportQueryService"/> class.
    /// </summary>
    /// <param name="requestService">The request service.</param>
    /// <param name="logger">The logger instance.</param>
    public DataExportQueryService(IRequestService requestService, ILogger<DataExportQueryService> logger)
    {
        ArgumentNullException.ThrowIfNull(requestService);
        ArgumentNullException.ThrowIfNull(logger);
        _requestService = requestService;
        _logger = logger;
    }

    [LoggerMessage(EventId = 1, Level = LogLevel.Information, Message = "GetSummariesAsync: received {Count} summaries.")]
    public static partial void LogGetSummaries(ILogger logger, int count);

    /// <inheritdoc/>
    public async Task<DataExportDetailsViewModel> GetDetailsAsync(ClaimsPrincipal user, string id, CancellationToken cancellationToken)
        => CheckValidResult((await _requestService.SubmitAsync(user, new GetDataExportDetails(id), cancellationToken)
            .ConfigureAwait(false)).Result);

    /// <inheritdoc/>
    public async Task<IdDescription> GetIdDescriptionAsync(ClaimsPrincipal user, string id, CancellationToken cancellationToken)
        => CheckValidResult((await _requestService.SubmitAsync(user, new GetDataExportIdDescription(id), cancellationToken)
            .ConfigureAwait(false)).Result);

    /// <inheritdoc/>
    public async Task<IEnumerable<IdDescription>> GetIdDescriptionsAsync(ClaimsPrincipal user, int skip, int take, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(user);
        IEnumerable<string> ids = CheckValidResult((await _requestService.SubmitAsync(user, new GetDataExportIds(skip, take), cancellationToken)
            .ConfigureAwait(false)).Result);
        List<Task<IdDescription>> tasks = [];
        foreach (string id in ids)
        {
            tasks.Add(GetIdDescriptionAsync(user, id, cancellationToken));
        }

        return await Task.WhenAll(tasks).ConfigureAwait(false);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<DataExportSummaryViewModel>> GetSummariesAsync(ClaimsPrincipal user, int skip, int take, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(user);

        GetDataExportSummaries request = await _requestService.SubmitAsync(user, new GetDataExportSummaries(skip, take), cancellationToken).ConfigureAwait(false);
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
    public async Task<IEnumerable<DataExportSummaryViewModel>> SearchSummariesAsync(ClaimsPrincipal user, string searchText, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(user);
        IEnumerable<DataExportSummaryViewModel> data = await GetSummariesAsync(user, 0, 0, cancellationToken).ConfigureAwait(false);
        if (!string.IsNullOrWhiteSpace(searchText))
        {
            data = data.Where(d =>
                d.Id.Contains(searchText, StringComparison.CurrentCultureIgnoreCase));
        }

        return data;
    }

    private static TResult CheckValidResult<TResult>([NotNull] TResult? result)
        => result ?? throw new InvalidOperationException("The request result is null or empty in data export query service");
}