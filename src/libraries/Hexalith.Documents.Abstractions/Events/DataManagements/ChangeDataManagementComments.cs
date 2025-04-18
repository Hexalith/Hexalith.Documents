// <copyright file="ChangeDataManagementComments.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Events.DataManagements;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents an event that occurs when the comments of a data management operation are changed.
/// </summary>
/// <param name="Id">The unique identifier of the data management operation.</param>
/// <param name="Comments">The new comments for the data management operation.</param>
[PolymorphicSerialization]
public partial record DataManagementCommentsChanged(
    string Id,
    [property: DataMember(Order = 2)] string? Comments)
    : DataManagementEvent(Id);