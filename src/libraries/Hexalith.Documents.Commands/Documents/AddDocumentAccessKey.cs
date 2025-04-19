// <copyright file="AddDocumentAccessKey.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Commands.Documents;

using System.Runtime.Serialization;

using Hexalith.Documents.ValueObjects;
using Hexalith.PolymorphicSerializations;

/// <summary>
/// Command to add an access key to a document.
/// </summary>
/// <param name="Id">The unique identifier of the document.</param>
/// <param name="AccessKey">The access key to be added to the document.</param>
[PolymorphicSerialization]
public partial record AddDocumentAccessKey(
    string Id,
    [property: DataMember(Order = 2)] DocumentAccessKey AccessKey) : DocumentCommand(Id);