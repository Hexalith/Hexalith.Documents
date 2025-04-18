// <copyright file="AddDocumentActor.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Commands.Documents;

using Hexalith.Documents.ValueObjects;
using Hexalith.PolymorphicSerializations;

[PolymorphicSerialization]
public partial record AddDocumentActor(string Id, DocumentActor Actor)
    : DocumentCommand(Id)
{
}