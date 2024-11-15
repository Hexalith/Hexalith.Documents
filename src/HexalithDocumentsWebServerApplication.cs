// <copyright file="AggregateFactory.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace HexalithApp.WebServer;

using System;
using System.Collections.Generic;

using Hexalith.Application.Modules.Applications;
using Hexalith.Documents.WebServer.Modules;
using Hexalith.Security.WebServer;

using HexalithApp.SharedUIElements;
using HexalithApp.WebApp;

/// <summary>
/// Represents a server application.
/// </summary>
public class HexalithDocumentsWebServerApplication : HexalithWebServerApplication
{
    /// <inheritdoc/>
    public override Type SharedUIElementsApplicationType => typeof(HexalithDocumentsSharedUIElementsApplication);

    /// <inheritdoc/>
    public override Type WebAppApplicationType => typeof(HexalithDocumentsWebAppApplication);

    /// <inheritdoc/>
    public override IEnumerable<Type> WebServerModules => [
        typeof(HexalithDocumentsWebServerModule),
        typeof(HexalithSecurityServerModule)];
}