namespace Hexalith.Documents.Server.Presentation.ViewModels;

public class DocumentAdd
{
    public string? Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? FileName { get; set; }

    public string? DocumentTypeId { get; set; }

    public IDictionary<string, string>? Tags { get; set; }
}