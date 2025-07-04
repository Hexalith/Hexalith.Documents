// <copyright file="HexalithDocumentsWebAppApplication.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace HexalithApp.WebApp;

using System;
using System.Collections.Generic;

using Hexalith.Application.Modules.Applications;
using Hexalith.Documents.Application;
using Hexalith.Documents.WebApp.Modules;
using Hexalith.Security.WebApp;
using Hexalith.UI.WebApp;

/// <summary>
/// Represents a client application.
/// </summary>
public class HexalithDocumentsWebAppApplication : HexalithWebAppApplication
{
    /// <inheritdoc/>
    public override string Id => $"{HexalithDocumentsApplicationInformation.Id}.{ApplicationType}";

    /// <inheritdoc/>
    public override string Name => $"{HexalithDocumentsApplicationInformation.Name} {ApplicationType}";

    /// <inheritdoc/>
    public override string ShortName => HexalithDocumentsApplicationInformation.ShortName;

    /// <inheritdoc/>
    public override IEnumerable<Type> WebAppModules
        => [
            typeof(HexalithUIComponentsWebAppModule),
            typeof(HexalithDocumentsWebAppModule),
            typeof(HexalithSecurityWebAppModule)];
}