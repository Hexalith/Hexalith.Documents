// <copyright file="DocumentTypeDataExtractionRemoved.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Events.DocumentTypes;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents an event that is raised when a data extraction configuration is removed from a document type.
/// </summary>
/// <param name="Id">The unique identifier of the document type.</param>
/// <param name="DataInformationExtractionId">The identifier of the data extraction configuration that was removed.</param>
[PolymorphicSerialization]
public partial record DocumentTypeDataExtractionRemoved(
    string Id,
    [property: DataMember(Order = 2)]
    string DataInformationExtractionId) : DocumentTypeEvent(Id)
{
}