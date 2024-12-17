namespace Hexalith.Documents.UI.Services.DocumentTypes.Services;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using System.Threading.Tasks;

using Hexalith.Application.Services;
using Hexalith.Documents.UI.Services.DocumentTypes.ViewModels;

/// <summary>
/// Represents an in-memory implementation of the document query service.
/// </summary>
public class MemoryDocumentTypeQueryService : IDocumentTypeQueryService
{
    private readonly IEnumerable<DocumentTypeDetailsViewModel> _data;

    /// <summary>
    /// Initializes a new instance of the <see cref="MemoryDocumentTypeQueryService"/> class with an empty data set.
    /// </summary>
    public MemoryDocumentTypeQueryService()
        : this([])
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MemoryDocumentTypeQueryService"/> class with the specified data.
    /// </summary>
    /// <param name="data">The initial set of document details.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="data"/> is null.</exception>
    public MemoryDocumentTypeQueryService([NotNull] IEnumerable<DocumentTypeDetailsViewModel> data)
    {
        ArgumentNullException.ThrowIfNull(data);
        _data = data;
    }

    /// <inheritdoc/>
    public Task<DocumentTypeDetailsViewModel> GetDetailsAsync(string id)
        => Task.FromResult(_data.Single(p => p.Id == id));

    /// <inheritdoc/>
    public Task<IdDescription> GetIdDescriptionAsync(ClaimsPrincipal user, string id, CancellationToken cancellationToken)
        => Task.FromResult(_data.Select(p => new IdDescription(p.Id, p.Name)).Single(d => d.Id == id));

    /// <inheritdoc/>
    public Task<IEnumerable<IdDescription>> GetIdDescriptionsAsync(ClaimsPrincipal user, int skip, int take, CancellationToken cancellationToken)
    {
        IQueryable<IdDescription> result = _data
            .Select(p => new IdDescription(p.Id, p.Name))
            .OrderBy(p => p.Description)
            .AsQueryable();
        if (skip > 0)
        {
            result = result.Skip(skip);
        }

        if (take > 0)
        {
            result = result.Take(take);
        }

        return Task.FromResult<IEnumerable<IdDescription>>([.. result]);
    }

    /// <inheritdoc/>
    public Task<IEnumerable<DocumentTypeSummaryViewModel>> GetSummariesAsync(int skip, int count)
    {
        IEnumerable<DocumentTypeDetailsViewModel> factories = _data;
        if (skip > 0)
        {
            factories = factories.Skip(skip);
        }

        if (count > 0)
        {
            factories = factories.Take(count);
        }

        return Task.FromResult(factories.Select(p => new DocumentTypeSummaryViewModel(p)));
    }

    /// <inheritdoc/>
    public Task<IEnumerable<IdDescription>> SearchIdDescriptionsAsync(ClaimsPrincipal user, string searchText, int skip, int take, CancellationToken cancellationToken)
    {
        IQueryable<IdDescription> result = _data
            .Select(p => new IdDescription(p.Id, p.Name))
            .Where(p =>
                p.Description.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                p.Id.Contains(searchText, StringComparison.OrdinalIgnoreCase))
            .OrderBy(p => p.Description)
            .AsQueryable();
        if (skip > 0)
        {
            result = result.Skip(skip);
        }

        if (take > 0)
        {
            result = result.Take(take);
        }

        List<IdDescription> list = [.. result];
        return Task.FromResult<IEnumerable<IdDescription>>(list);
    }

    /// <inheritdoc/>
    public Task<IEnumerable<DocumentTypeSummaryViewModel>> SearchSummariesAsync(string searchText)
    {
        IEnumerable<DocumentTypeDetailsViewModel> factories = _data;
        if (!string.IsNullOrWhiteSpace(searchText))
        {
            factories = factories.Where(f =>
                f.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                f.Id.Contains(searchText, StringComparison.OrdinalIgnoreCase));
        }

        return Task.FromResult(factories.Select(p => new DocumentTypeSummaryViewModel(p)));
    }
}