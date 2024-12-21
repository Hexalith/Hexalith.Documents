namespace Hexalith.Documents.Application;

using System.Threading.Tasks;

using Hexalith.Documents.Domain.DocumentContainers;

/// <summary>
/// Defines the contract for user data services.
/// </summary>
public interface IUserDataService
{
    /// <summary>
    /// Gets the user document container identifier.
    /// </summary>
    /// <param name="partitionId">The partition identifier.</param>"
    /// <param name="userId">The user identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the user document container identifier.</returns>
    Task<DocumentContainer> GetUserDocumentContainerGlobalIdAsync(string partitionId, string userId, CancellationToken cancellationToken);
}