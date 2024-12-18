﻿namespace Hexalith.Documents.UI.Services.DocumentTypes.ViewModels;

/// <summary>
/// Represents detailed information about a document type.
/// </summary>
/// <param name="Id">The unique identifier of the document type.</param>
/// <param name="Name">The name of the document type.</param>
/// <param name="Description">The description of the document type.</param>
/// <param name="Disabled">Indicates whether the document type is disabled.</param>
public record DocumentTypeDetailsViewModel(string Id, string Name, string? Description, bool Disabled);