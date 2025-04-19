// <copyright file="DocumentAccessKey.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.ValueObjects;

using System;
using System.Runtime.Serialization;

/// <summary>
/// Represents a key to access a document with an expiration date.
/// </summary>
/// <param name="Key">The access key.</param>
/// <param name="ValidUntil">The expiration date of the access key.</param>
[DataContract]
public record DocumentAccessKey(
    [property: DataMember(Order = 2)] string Key,
    [property: DataMember(Order = 3)] DateTimeOffset ValidUntil);