// <copyright file="AggregateFactory.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace HexalithApp.SharedAssets;

using Hexalith.Application.Modules.Applications;
using Hexalith.Documents.SharedAssets.Modules;
using Hexalith.EasyAuthentication.SharedAssets.Modules;
using Hexalith.Extensions.Helpers;
using Hexalith.UI.Components.Modules;

/// <summary>
/// Represents a shared application.
/// </summary>
public class HexalithDocumentsSharedAssetsApplication : HexalithSharedAssetsApplication
{
    /// <inheritdoc/>
    public override string HomePath => "Document";

    /// <inheritdoc/>
    public override string Id => "Document";

    /// <inheritdoc/>
    public override string LoginPath => "/.auth/login/aad";

    /// <inheritdoc/>
    public override string LogoutPath => "/.auth/logout";

    /// <inheritdoc/>
    public override string Name => "Documents";

    /// <inheritdoc/>
    public override IEnumerable<Type> SharedAssetsModules =>
    [
        typeof(HexalithDocumentsSharedAssetsModule),
        typeof(HexalithUIComponentsSharedModule),
        typeof(HexalithEasyAuthenticationSharedModule),
    ];

    /// <inheritdoc/>
    public override string Version => VersionHelper.ProductVersion<HexalithUIComponentsSharedModule>() ?? "?.?.?";
}