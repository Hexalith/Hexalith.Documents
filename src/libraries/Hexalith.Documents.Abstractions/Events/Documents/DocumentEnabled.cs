// <copyright file="DocumentEnabled.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Events.Documents;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents a document enabled event.
/// </summary>
[PolymorphicSerialization]
public partial record DocumentEnabled(string Id) : DocumentEvent(Id);