namespace Hexalith.Documents.Application.Services;

using System.Threading;
using System.Threading.Tasks;

using Hexalith.Documents.Application;
using Hexalith.Documents.Domain.DocumentContainers;

/// <summary>
/// Service to handle user data related operations.
/// </summary>
public class UserDataService : IUserDataService
{
    /// <summary>
    /// Gets the global ID of the user's document container.
    /// </summary>
    /// <param name="partitionId">The partition identifier.</param>
    /// <param name="userId">The user identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the document container.</returns>
    public Task<DocumentContainer> GetUserDocumentContainerGlobalIdAsync(
        string partitionId,
        string userId,
        CancellationToken cancellationToken) => Task.FromResult<DocumentContainer>(new DocumentContainer());
}