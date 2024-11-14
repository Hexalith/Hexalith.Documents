// <copyright file="AggregateFactory.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace HexalithApp.WebServer;

using System;
using System.Collections.Generic;

using Hexalith.Application.Modules.Applications;
using Hexalith.Documents.WebServer.Modules;
using Hexalith.EasyAuthentication.WebServer;

using HexalithApp.SharedAssets;
using HexalithApp.WebApp;

/// <summary>
/// Represents a server application.
/// </summary>
public class HexalithDocumentsWebServerApplication : HexalithWebServerApplication
{
    /// <inheritdoc/>
    public override Type SharedAssetsApplicationType => typeof(HexalithDocumentsSharedAssetsApplication);

    /// <inheritdoc/>
    public override Type WebAppApplicationType => typeof(HexalithDocumentsWebAppApplication);

    /// <inheritdoc/>
    public override IEnumerable<Type> WebServerModules => [
        typeof(HexalithDocumentsWebServerModule),
        typeof(HexalithEasyAuthenticationServerModule)];
}