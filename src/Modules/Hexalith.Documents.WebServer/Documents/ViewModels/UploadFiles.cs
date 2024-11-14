namespace Hexalith.Documents.WebServer.Documents.ViewModels;

public class UploadFiles
{
    public string? Description { get; set; }
    public string? DocumentTypeId { get; set; }
    public string? FileName { get; set; }
    public string? Id { get; set; }

    public string? Name { get; set; }
    public IDictionary<string, string>? Tags { get; set; }
}