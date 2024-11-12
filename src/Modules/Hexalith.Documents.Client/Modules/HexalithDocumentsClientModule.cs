// <copyright file="DocumentClientModule.cs" company="Hexalith SAS Paris France">
//     Copyright (c) Hexalith SAS Paris France. All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Client.Modules;

using System.Collections.Generic;
using System.Reflection;

using Hexalith.Application.Modules.Modules;

using Microsoft.Extensions.Configuration;

using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// The document construction site client module.
/// </summary>
public class HexalithDocumentsClientModule : IClientApplicationModule
{
    /// <inheritdoc/>
    public IEnumerable<string> Dependencies => [];

    /// <inheritdoc/>
    public string Description => "Document client module";

    /// <inheritdoc/>
    public string Id => "Hexalith.Document.Client";

    /// <inheritdoc/>
    public string Name => "Document client";

    /// <inheritdoc/>
    public int OrderWeight => 0;

    /// <inheritdoc/>
    public string Path => "Document";

    /// <inheritdoc/>
    public IEnumerable<Assembly> PresentationAssemblies => [GetType().Assembly];

    /// <inheritdoc/>
    public string Version => "1.0";

    /// <summary>
    /// Adds services to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration">The configuration.</param>
    public static void AddServices(IServiceCollection services, IConfiguration configuration)
    {
    }

    /// <inheritdoc/>
    public void UseModule(object builder)
    {
    }
}