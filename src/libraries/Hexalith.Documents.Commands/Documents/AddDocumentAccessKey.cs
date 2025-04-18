// <copyright file="AddDocumentAccessKey.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Commands.Documents;

using System.Runtime.Serialization;

using Hexalith.Documents.ValueObjects;
using Hexalith.PolymorphicSerializations;

[PolymorphicSerialization]
public partial record AddDocumentAccessKey(
    string Id,
    [property: DataMember(Order = 2)] DocumentAccessKey AccessKey) : DocumentCommand(Id)
{
}