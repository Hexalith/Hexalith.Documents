﻿namespace Hexalith.Documents.Commands;

using System.Runtime.Serialization;

using Hexalith.Document.Domain.ValueObjects;
using Hexalith.PolymorphicSerialization;

/// <summary>
/// Command to create a new document.
/// </summary>
/// <param name="Id">The unique identifier of the document.</param>
/// <param name="Name">The name of the document.</param>
/// <param name="Description">The description of the document.</param>
/// <param name="File">The the file description.</param>
/// <param name="OwnerId">The identifier of the document owner.</param>
/// <param name="CreatedOn">The date and time when the document was created.</param>
/// <param name="DocumentTypeId">The identifier of the document type.</param>
[PolymorphicSerialization]
public partial record CreateDocument(
    string Id,
    [property: DataMember(Order = 2)]
    string Name,
    [property: DataMember(Order = 3)]
    string Description,
    [property: DataMember(Order = 4)]
    FileDescription File,
    string OwnerId,
    [property: DataMember(Order = 5)]
    DateTimeOffset CreatedOn,
    [property: DataMember(Order = 6)]
    string DocumentTypeId)
    : DocumentCommand(Id)
{
}