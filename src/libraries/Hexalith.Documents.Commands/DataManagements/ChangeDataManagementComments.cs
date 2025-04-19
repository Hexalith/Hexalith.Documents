// <copyright file="ChangeDataManagementComments.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Commands.DataManagements;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents a command to change the comments of a data management entity.
/// </summary>
/// <param name="Id">The unique identifier of the data management entity.</param>
/// <param name="Comments">The new comments to associate with the data management entity.</param>
[PolymorphicSerialization]
public partial record ChangeDataManagementComments(
    string Id,
    [property: DataMember(Order = 2)] string? Comments)
    : DataManagementCommand(Id);