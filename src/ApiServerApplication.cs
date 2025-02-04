// <copyright file="AggregateFactory.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace HexalithApp.Server;

using System;
using System.Collections.Generic;

using Hexalith.Application.Modules.Applications;
using Hexalith.Documents.ApiServer.Modules;
using Hexalith.EasyAuthentication.ApiServer;

using HexalithApp.Shared;

/// <summary>
/// Represents a server application.
/// </summary>
public class HexalithDocumentsApiServerApplication : HexalithApiServerApplication
{
    /// <inheritdoc/>
    public override IEnumerable<Type> ApiServerModules => [
        typeof(HexalithDocumentsApiServerModule),
        typeof(HexalithEasyAuthenticationApiServerModule)];

    /// <inheritdoc/>
    public override Type SharedAssetsApplicationType => typeof(SharedAssetsApplication);
}