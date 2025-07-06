// <copyright file="HexalithDocumentsWebServerApplication.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace HexalithApp.WebServer;

using System;
using System.Collections.Generic;

using Hexalith.Application.Modules.Applications;
using Hexalith.Documents.Application;
using Hexalith.Documents.WebServer.Modules;
using Hexalith.Security.WebServer;
using Hexalith.UI.WebServer;

using HexalithApp.WebApp;

/// <summary>
/// Represents a server application.
/// </summary>
public class HexalithDocumentsWebServerApplication : HexalithWebServerApplication
{
    /// <inheritdoc/>
    public override string Id => $"{HexalithDocumentsApplicationInformation.Id}.{ApplicationType}";

    /// <inheritdoc/>
    public override string Name => $"{HexalithDocumentsApplicationInformation.Name} {ApplicationType}";

    /// <inheritdoc/>
    public override string ShortName => HexalithDocumentsApplicationInformation.ShortName;

    /// <inheritdoc/>
    public override Type WebAppApplicationType => typeof(HexalithDocumentsWebAppApplication);

    /// <inheritdoc/>
    public override IEnumerable<Type> WebServerModules => [
        typeof(HexalithUIComponentsWebServerModule),
        typeof(HexalithDocumentsWebServerModule),
        typeof(HexalithSecurityWebServerModule)];
}