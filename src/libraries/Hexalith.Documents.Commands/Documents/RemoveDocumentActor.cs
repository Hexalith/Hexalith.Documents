// <copyright file="RemoveDocumentActor.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Commands.Documents;

using Hexalith.PolymorphicSerializations;

[PolymorphicSerialization]
public partial record RemoveDocumentActor(string Id, string ContactId)
    : DocumentCommand(Id)
{
}