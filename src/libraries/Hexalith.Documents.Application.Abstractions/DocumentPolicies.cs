// <copyright file="DocumentPolicies.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Application;

/// <summary>
/// Defines the policies for document security within the application.
/// </summary>
public static class DocumentPolicies
{
    /// <summary>
    /// Policy for users who can contribute to documents.
    /// </summary>
    public const string Contributor = DocumentRoles.Contributor;

    /// <summary>
    /// Policy for users who own documents.
    /// </summary>
    public const string Owner = DocumentRoles.Owner;

    /// <summary>
    /// Policy for users who can read documents.
    /// </summary>
    public const string Reader = DocumentRoles.Reader;
}