namespace Hexalith.Documents.Application.Helpers;

using System.Collections.Generic;

using Hexalith.Application;

using Microsoft.AspNetCore.Authorization;

/// <summary>
/// Provides authorization policies for the document module.
/// </summary>
public static class DocumentModulePolicies
{
    /// <summary>
    /// Gets the authorization policies for the document module.
    /// </summary>
    public static IDictionary<string, AuthorizationPolicy> AuthorizationPolicies =>
    new Dictionary<string, AuthorizationPolicy>
    {
        {
            DocumentPolicies.Owners, new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .RequireRole(ApplicationRoles.GlobalAdministrator, DocumentRoles.Owner)
                .Build()
        },
        {
            DocumentPolicies.Contributors, new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .RequireRole(ApplicationRoles.GlobalAdministrator, DocumentRoles.Owner, DocumentRoles.Contributor)
                .Build()
        },
        {
            DocumentPolicies.Readers, new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .RequireRole(ApplicationRoles.GlobalAdministrator, DocumentRoles.Owner, DocumentRoles.Contributor, DocumentRoles.Reader)
                .Build()
        },
    };

    /// <summary>
    /// Adds the document module policies to the specified authorization options.
    /// </summary>
    /// <param name="options">The authorization options to add the policies to.</param>
    /// <returns>The updated authorization options.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the options parameter is null.</exception>
    public static AuthorizationOptions AddDocumentAuthorizationPolicies(this AuthorizationOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);
        foreach (KeyValuePair<string, AuthorizationPolicy> policy in AuthorizationPolicies)
        {
            options.AddPolicy(policy.Key, policy.Value);
        }

        return options;
    }
}