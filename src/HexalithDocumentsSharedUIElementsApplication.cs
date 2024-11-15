// <copyright file="AggregateFactory.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace HexalithApp.SharedUIElements;

using Hexalith.Documents.SharedUIElements.Modules;
using Hexalith.Extensions.Helpers;
using Hexalith.UI.Components.Modules;

/// <summary>
/// Represents a shared application.
/// </summary>
public class HexalithDocumentsSharedUIElementsApplication : HexalithSharedUIElementsApplication
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
    public override IEnumerable<Type> SharedUIElementsModules =>
    [
        typeof(HexalithDocumentsSharedUIElementsModule),
        typeof(HexalithUIComponentsSharedModule),
        typeof(HexalithEasyAuthenticationSharedModule),
    ];

    /// <inheritdoc/>
    public override string Version => VersionHelper.ProductVersion<HexalithUIComponentsSharedModule>() ?? "?.?.?";
}