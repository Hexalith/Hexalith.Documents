namespace Hexalith.Documents.Servers.Services;

using System.IO;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Documents.Application.Services;

/// <summary>
/// Represents a local storage file implementation.
/// </summary>
/// <param name="fileStream">The file stream used for reading and writing.</param>
/// <param name="fileUrl">The URL that identifies the location of the file.</param>
public class LocalStorageFile(FileStream fileStream, Uri fileUrl) : IWritableFile
{
    /// <inheritdoc/>
    public Uri FileUrl { get; } = fileUrl;

    /// <inheritdoc/>
    public long Size { get; private set; }

    /// <inheritdoc/>
    public Stream Stream { get; } = fileStream;

    /// <inheritdoc/>
    public async Task<long> CloseAsync(CancellationToken cancellationToken)
    {
        await Stream.FlushAsync(cancellationToken).ConfigureAwait(false);
        Stream.Close();
        return Size = Stream.Length;
    }

    /// <inheritdoc/>
    public async ValueTask DisposeAsync()
    {
        if (Stream is not null)
        {
            _ = await CloseAsync(CancellationToken.None).ConfigureAwait(false);
            await Stream.DisposeAsync().ConfigureAwait(false);
        }

        GC.SuppressFinalize(this);
    }
}