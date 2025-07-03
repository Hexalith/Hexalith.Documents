// <copyright file="HexalithDocumentsApiServerApplication.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace HexalithApp.ApiServer;

using System;
using System.Collections.Generic;

using Hexalith.Application.Modules.Applications;
using Hexalith.Documents.ApiServer.Modules;
using Hexalith.Documents.Application;
using Hexalith.Security.ApiServer;
using Hexalith.UI.ApiServer;

/// <summary>
/// Represents a server application.
/// </summary>
public class HexalithDocumentsApiServerApplication : HexalithApiServerApplication
{
    /// <inheritdoc/>
    public override IEnumerable<Type> ApiServerModules => [
        typeof(HexalithUIComponentsApiServerModule),
        typeof(HexalithDocumentsApiServerModule),
        typeof(HexalithSecurityApiServerModule)];

    /// <inheritdoc/>
    public override string Id => $"{HexalithDocumentsApplicationInformation.Id}.{ApplicationType}";

    /// <inheritdoc/>
    public override string Name => $"{HexalithDocumentsApplicationInformation.Name} {ApplicationType}";

    /// <inheritdoc/>
    public override string ShortName => HexalithDocumentsApplicationInformation.ShortName;
}