// <copyright file="GetDataManagementIdDescription.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Requests.DataManagements;

using System.Runtime.Serialization;

using Hexalith.Domains.ValueObjects;
using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents a request to get the description of a data export by its ID.
/// </summary>
/// <param name="Id">The ID of the data export.</param>
/// <param name="Result">The result containing the ID and description.</param>
[PolymorphicSerialization]
public partial record GetDataManagementIdDescription(string Id, [property: DataMember(Order = 2)] IdDescription? Result = null)
    : DataManagementRequest(Id);