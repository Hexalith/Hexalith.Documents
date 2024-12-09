namespace Hexalith.Documents.Application.FileTypes.Services;

using System.Collections.Generic;
using System.Threading.Tasks;

/// <summary>
/// Defines a service for managing file type collections.
/// </summary>
public interface IFileTypeCollectionService
{
    /// <summary>
    /// Gets a collection of file type IDs.
    /// </summary>
    /// <param name="skip">The number of items to skip.</param>
    /// <param name="take">The number of items to take.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of file type IDs.</returns>
    Task<IEnumerable<string?>> GetIdsAsync(int skip, int take);
}