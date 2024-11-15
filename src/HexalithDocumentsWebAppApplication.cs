// <copyright file="AggregateFactory.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace HexalithApp.WebApp;

using System;
using System.Collections.Generic;

using Hexalith.Application.Modules.Applications;
using Hexalith.Documents.WebApp.Modules;
using Hexalith.Security.WebApp;

using HexalithApp.SharedUIElements;

/// <summary>
/// Represents a client application.
/// </summary>
public class HexalithDocumentsWebAppApplication : HexalithWebAppApplication
{
    /// <inheritdoc/>
    public override Type SharedUIElementsApplicationType => typeof(HexalithDocumentsSharedUIElementsApplication);

    /// <inheritdoc/>
    public override IEnumerable<Type> WebAppModules
        => [
            typeof(HexalithDocumentsWebAppModule),
            typeof(HexalithSecurityWebAppModule)];
}