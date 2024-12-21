namespace Hexalith.Documents.Application.Services;

using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Represents a writable file stream that supports asynchronous disposal.
/// </summary>
public interface IWritableFile : IDisposable
{
    /// <summary>
    /// Gets the writable stream.
    /// </summary>
    Stream Stream { get; }

    /// <summary>
    /// Closes the writable file asynchronously.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous close operation. The task result contains the length of the stream.</returns>
    Task<long> CloseAsync(CancellationToken cancellationToken);
}