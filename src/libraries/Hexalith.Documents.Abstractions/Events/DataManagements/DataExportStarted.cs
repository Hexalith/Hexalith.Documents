// <copyright file="DataExportStarted.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Events.DataManagements;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents an event that occurs when a data export operation is started.
/// </summary>
/// <param name="Id">The unique identifier of the data management operation.</param>
/// <param name="DateTime">The date and time when the export operation started.</param>
[PolymorphicSerialization]
public partial record DataExportStarted(
    string Id,
    [property: DataMember(Order = 2)]
    DateTimeOffset DateTime)
    : DataManagementEvent(Id);