﻿namespace Hexalith.Documents.UI.Pages.FileTypes.Services;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

using Hexalith.Documents.UI.Services.FileTypes.Services;
using Hexalith.Documents.UI.Services.FileTypes.ViewModels;
using Hexalith.UI.Components.ViewModels;

/// <summary>
/// Represents an in-memory implementation of the document query service.
/// </summary>
public class MemoryFileTypeQueryService : IFileTypeQueryService
{
    private readonly IEnumerable<FileTypeDetailsViewModel> _data;

    /// <summary>
    /// Initializes a new instance of the <see cref="MemoryFileTypeQueryService"/> class with an empty data set.
    /// </summary>
    public MemoryFileTypeQueryService()
        : this([])
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MemoryFileTypeQueryService"/> class with the specified data.
    /// </summary>
    /// <param name="data">The initial set of document details.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="data"/> is null.</exception>
    public MemoryFileTypeQueryService([NotNull] IEnumerable<FileTypeDetailsViewModel> data)
    {
        ArgumentNullException.ThrowIfNull(data);
        _data = data;
    }

    /// <inheritdoc/>
    public Task<FileTypeDetailsViewModel> GetDetailsAsync(string id)
        => Task.FromResult(_data.Single(p => p.Id == id));

    /// <inheritdoc/>
    public Task<IdDescription> GetIdDescriptionAsync(string id, CancellationToken cancellationToken)
        => Task.FromResult(_data.Select(p => new IdDescription(p.Id, p.Name)).Single(d => d.Id == id));

    /// <inheritdoc/>
    public Task<IEnumerable<IdDescription>> GetIdDescriptionsAsync(int skip, int count, CancellationToken cancellationToken)
    {
        IQueryable<IdDescription> result = _data
            .Select(p => new IdDescription(p.Id, p.Name))
            .OrderBy(p => p.Description)
            .AsQueryable();
        if (skip > 0)
        {
            result = result.Skip(skip);
        }

        if (count > 0)
        {
            result = result.Take(count);
        }

        return Task.FromResult<IEnumerable<IdDescription>>([.. result]);
    }

    /// <inheritdoc/>
    public Task<IEnumerable<FileTypeSummaryViewModel>> GetSummariesAsync(int skip, int count)
    {
        IEnumerable<FileTypeDetailsViewModel> factories = _data;
        if (skip > 0)
        {
            factories = factories.Skip(skip);
        }

        if (count > 0)
        {
            factories = factories.Take(count);
        }

        return Task.FromResult(factories.Select(p => new FileTypeSummaryViewModel(p)));
    }

    /// <inheritdoc/>
    public Task<IEnumerable<IdDescription>> SearchIdDescriptionsAsync(string searchText, int skip, int count, CancellationToken cancellationToken)
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

        if (count > 0)
        {
            result = result.Take(count);
        }

        List<IdDescription> list = result.ToList();
        return Task.FromResult<IEnumerable<IdDescription>>(list);
    }

    /// <inheritdoc/>
    public Task<IEnumerable<FileTypeSummaryViewModel>> SearchSummariesAsync(string searchText)
    {
        IEnumerable<FileTypeDetailsViewModel> factories = _data;
        if (!string.IsNullOrWhiteSpace(searchText))
        {
            factories = factories.Where(f =>
                f.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                f.Id.Contains(searchText, StringComparison.OrdinalIgnoreCase));
        }

        return Task.FromResult(factories.Select(p => new FileTypeSummaryViewModel(p)));
    }
}