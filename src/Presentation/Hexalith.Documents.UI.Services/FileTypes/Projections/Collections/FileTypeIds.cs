namespace Hexalith.Documents.UI.Services.FileTypes.Projections.Collections;

using System.Collections.Generic;
using System.Runtime.Serialization;

using Hexalith.Application.Projections;

/// <summary>
/// Represents a collection of file type IDs with pagination support.
/// </summary>
/// <param name="NextPageId">The ID of the next page.</param>
/// <param name="Ids">The collection of file type IDs.</param>
[DataContract]
public record FileTypeIds(string? NextPageId, IEnumerable<string> Ids) : IdCollection(NextPageId, Ids);