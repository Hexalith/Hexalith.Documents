﻿// <copyright file="DocumentInformationExtractionEvent.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Events.DocumentInformationExtractions;

using System.Runtime.Serialization;

using Hexalith.Documents;
using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents a base class for document commands.
/// </summary>
[PolymorphicSerialization]
public abstract partial record DocumentInformationExtractionEvent([property: DataMember(Order = 1)] string Id)
{
    /// <summary>
    /// Gets the aggregate ID.
    /// </summary>
    /// <value>The aggregate ID.</value>
    public string AggregateId => Id;

    /// <summary>
    /// Gets the aggregate name.
    /// </summary>
    /// <value>The aggregate name.</value>
    public static string AggregateName => DocumentDomainHelper.DocumentInformationExtractionAggregateName;
}