namespace Hexalith.Documents.UI.Services.FileTypes.ViewModels;

using System.Collections.Generic;

/// <summary>
/// Represents the details of a file type.
/// </summary>
/// <param name="Id">The unique identifier of the file type.</param>
/// <param name="Name">The name of the file type.</param>
/// <param name="Description">The description of the file type.</param>
/// <param name="FileToTextConverter">The file type file to text converter.</param>
/// <param name="Targets">The targets associated with the file type.</param>
/// <param name="Disabled">Indicates whether the file type is disabled.</param>
public record FileTypeDetailsViewModel(string Id, string Name, string? Description, string? FileToTextConverter, IEnumerable<string> Targets, bool Disabled)
{
}