namespace Hexalith.Documents.UI.Services.ImportExport.Services;

using System;
using System.Threading.Tasks;

/// <summary>
/// Defines the interface for importing files.
/// </summary>
public interface IImportFileService
{
    /// <summary>
    /// Imports a file from the specified URL asynchronously.
    /// </summary>
    /// <param name="fileUrl">The URL of the file to import.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the number of imported records.</returns>
    Task<int> ImportFileAsync(Uri fileUrl);
}